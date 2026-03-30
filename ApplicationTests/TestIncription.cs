using ESP.Application.UseCases;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class TestIncription
    {
        private Mock<IInscriptionRepository> _inscriptionRepositoryMock = null!;
        private Mock<IStripeService> _stripeServiceMock = null!;
        private Mock<ILogger<InscriptionCourseUseCase>> _loggerMock = null!;
        private InscriptionCourseUseCase _inscriptionCourseUseCase = null!;

        [SetUp]
        public void Setup()
        {
            _inscriptionRepositoryMock = new Mock<IInscriptionRepository>();
            _stripeServiceMock = new Mock<IStripeService>();
            _loggerMock = new Mock<ILogger<InscriptionCourseUseCase>>();

            _inscriptionCourseUseCase = new InscriptionCourseUseCase(
                _inscriptionRepositoryMock.Object,
                _stripeServiceMock.Object
            );
        }

        [Test]
        public async Task Inscription_ShouldAddRegistration_WhenPaymentIsValid()
        {
            // Arrange
            var json = "json";
            var signature = "signature";

            var metadata = new Dictionary<string, string>
            {
                { "idRace", "1" }
            };

            _stripeServiceMock
                .Setup(x => x.VerifyAndExtractAsync(json, signature))
                .ReturnsAsync(metadata);

            // AlreadyExistsAsync ne prend plus idUser mais les infos complètes du participant
            _inscriptionRepositoryMock
                .Setup(x => x.AlreadyExistsAsync(
                    It.IsAny<int>(),  // idRace
                    It.IsAny<string>(), // nom
                    It.IsAny<string>(), // prenom
                    It.IsAny<DateOnly>(), // dateNaissance
                    It.IsAny<string>() // adresseMail
                ))
                .ReturnsAsync(false);

            _inscriptionRepositoryMock
                .Setup(x => x.GenerateBibNumberAsync(It.IsAny<int>()))
                .ReturnsAsync(42);

            // Act
            await _inscriptionCourseUseCase.ExecuteAsync(json, signature);

            // Assert
            _inscriptionRepositoryMock.Verify(
                x => x.AddAsync(It.Is<Registration>(r =>
                    r.IdRace == 1 &&
                    r.BibNumber == 42
                )),
                Times.Once
            );

            _inscriptionRepositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once
            );
        }
    }
}