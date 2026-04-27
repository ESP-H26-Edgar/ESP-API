using ESP.Application.UseCases;
using ESP.Application.DTOS;
using ESP.Application.Services;    
using ESP.Domain.Interfaces.Repositories;
using ESP.Infrastructure;            
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationTests
{
        //Test réalisé en colaboration avec l'ia, j'ai fais le premier test et il m'a conseiller et aider pour les autres
    public class TestRace
    {
        private Mock<IRaceRepository> _raceRepositoryMock = null!;
        private Mock<IImageService> _imageServiceMock = null!;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock = null!;
        private Mock<ILogger<CreateRaceUseCase>> _createLoggerMock = null!;
        private Mock<ILogger<DeleteRaceUseCase>> _deleteLoggerMock = null!;
        private CreateRaceUseCase _createRaceUseCase = null!;
        private DeleteRaceUseCase _deleteRaceUseCase = null!;

        [SetUp]
        public void Setup()
        {
            _raceRepositoryMock = new Mock<IRaceRepository>();
            _imageServiceMock = new Mock<IImageService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _createLoggerMock = new Mock<ILogger<CreateRaceUseCase>>();
            _deleteLoggerMock = new Mock<ILogger<DeleteRaceUseCase>>();

            var claims = new[]
            {
        new Claim("idUser", "1"),
        new Claim(ClaimTypes.Email, "edgar@gmail.com")
    };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

         
            _imageServiceMock
                .Setup(x => x.SaveImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("images/test.jpg");

            _createRaceUseCase = new CreateRaceUseCase(
                _raceRepositoryMock.Object,
                _imageServiceMock.Object,
                _createLoggerMock.Object,
                _httpContextAccessorMock.Object
            );

            _deleteRaceUseCase = new DeleteRaceUseCase(
                _raceRepositoryMock.Object,
                _deleteLoggerMock.Object,
                _httpContextAccessorMock.Object
            );
        }



        [Test]
        public async Task CreateRace_ShouldAddRace_WhenRequestIsValid()
        {
            var request = new CreateRaceRequest
            {
                RaceName = "Marathon de Montréal",
                IdRaceType = 1,
                Kilometer = 42,
                Location = "Montréal, QC",
                Date = DateTime.UtcNow.AddMonths(3),
                Description = "Une belle course",
                NumberPlace = 500,
                Price = 75,
                Image = null 
            };

            var expectedRace = new Race
            {
                IdRace = 1,
                RaceName = request.RaceName,
                Kilometer = request.Kilometer,
                Date = request.Date
            };

            _imageServiceMock
                .Setup(x => x.SaveImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("images/test.jpg");

            _raceRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Race>()))
                .ReturnsAsync(expectedRace);

            var result = await _createRaceUseCase.Execute(request);
            _raceRepositoryMock.Verify(
                x => x.AddAsync(It.Is<Race>(r =>
                    r.RaceName == request.RaceName &&
                    r.Kilometer == request.Kilometer &&
                    r.NumberPlace == request.NumberPlace
                )),
                Times.Once
            );

            Assert.That(result.IdRace, Is.EqualTo(1));
            Assert.That(result.RaceName, Is.EqualTo("Marathon de Montréal"));
        }

        [Test]
        public void CreateRace_ShouldThrow_WhenNumberPlaceIsZero()
        {
            var request = new CreateRaceRequest
            {
                RaceName = "Marathon de Montréal",
                IdRaceType = 1,
                Kilometer = 42,
                Location = "Montréal, QC",
                Date = DateTime.UtcNow.AddMonths(3),
                Description = "Une belle course",
                NumberPlace = 0,
                Price = 75,
                Image = null
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(
                () => _createRaceUseCase.Execute(request)
            );
            Assert.That(ex!.Message, Does.Contain("NumberPlace must be greater than 0"));

            _raceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Race>()), Times.Never);
        }


        [Test]
        public async Task DeleteRace_ShouldDeleteRace_WhenRaceExists()
        {
            var race = new Race { IdRace = 1, RaceName = "Marathon de Montréal", Date = DateTime.UtcNow };

            _raceRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(race);

            _raceRepositoryMock
                .Setup(x => x.DeleteAsync(race))
                .Returns(Task.CompletedTask);

            var result = await _deleteRaceUseCase.ExecuteAsync(1);
            _raceRepositoryMock.Verify(x => x.DeleteAsync(It.Is<Race>(r => r.IdRace == 1)), Times.Once);
            Assert.That(result.IdRace, Is.EqualTo(1));
            Assert.That(result.RaceName, Is.EqualTo("Marathon de Montréal"));
        }

        [Test]
        public void DeleteRace_ShouldThrow_WhenRaceNotFound()
        {
 
            _raceRepositoryMock
                .Setup(x => x.GetByIdAsync(99))
                .ReturnsAsync((Race?)null);

            var ex = Assert.ThrowsAsync<Exception>(
                () => _deleteRaceUseCase.ExecuteAsync(99)
            );
            Assert.That(ex!.Message, Does.Contain("Race introuvable"));

            _raceRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Race>()), Times.Never);
        }
    }
}