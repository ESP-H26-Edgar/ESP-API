using ESP.Application.UseCases;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class TestIncription
    {
        private Mock<IInscriptionRepository> _inscriptionRepositoryMock;
        private Mock<IStripeService> _stripeServiceMock;
        private InscriptionCourseUseCase _inscriptionCourseUseCase;

        [SetUp]
        public void Setup()
        {
            _inscriptionRepositoryMock = new Mock<IInscriptionRepository>();
            _stripeServiceMock = new Mock<IStripeService>();
            _inscriptionCourseUseCase = new InscriptionCourseUseCase(
                _inscriptionRepositoryMock.Object,
                _stripeServiceMock.Object
            );
        }

        [Test]
        //aide de Claude pour le test ce qui m'a permis de savoir comment le faire
        public async Task Inscription_ShouldAddRegistration_WhenPaymentIsValid()
        {
            // Arrange
            var json = "json";
            var signature = "signature";

            var metadata = new Dictionary<string, string>
                {
                 { "idUser", "1" },
                 { "idRace", "1" }
                };

            _stripeServiceMock
                .Setup(x => x.VerifyAndExtractAsync(json, signature))
                .ReturnsAsync(metadata);

            _inscriptionRepositoryMock
            .Setup(x => x.AlreadyExistsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateOnly>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(false);

            _inscriptionRepositoryMock
                .Setup(x => x.GenerateBibNumberAsync(1))
                .ReturnsAsync(42);

            await _inscriptionCourseUseCase.ExecuteAsync(json, signature);

            _inscriptionRepositoryMock.Verify(
                x => x.AddAsync(It.Is<Registration>(r =>
                    r.IdUser == 1 &&
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