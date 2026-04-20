using ESP.Application.DTOS;
using ESP.Application.Services;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;
using ESP.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ESP.Application.UseCases
{
    public class CreateRaceUseCase : ICreateRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IImageService _imageService;
        private readonly ILogger<CreateRaceUseCase> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateRaceUseCase(
            IRaceRepository raceRepository,
            IImageService imageService,
            ILogger<CreateRaceUseCase> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _imageService = imageService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RaceDto> Execute(CreateRaceRequest request)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userId = user?.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;
            var email = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            _logger.LogInformation(
                "CreateRace started by User {UserId} ({Email})",
                userId,
                email
            );

            if (string.IsNullOrWhiteSpace(request.RaceName))
            {
                _logger.LogWarning("Race creation failed: missing RaceName by User {UserId}", userId);
                throw new ArgumentException("Race name is required");
            }

            if (request.Kilometer <= 0)
            {
                _logger.LogWarning("Race creation failed: invalid Kilometer ({Kilometer}) by User {UserId}", request.Kilometer, userId);
                throw new ArgumentException("Kilometer must be greater than 0");
            }

            if (request.NumberPlace <= 0)
            {
                _logger.LogWarning("Race creation failed: invalid NumberPlace ({NumberPlace}) by User {UserId}", request.NumberPlace, userId);
                throw new ArgumentException("NumberPlace must be greater than 0");
            }

            if (request.Price < 0)
            {
                _logger.LogWarning("Race creation failed: invalid Price ({Price}) by User {UserId}", request.Price, userId);
                throw new ArgumentException("Price cannot be negative");
            }

            var imagePath = await _imageService.SaveImage(request.Image);

            var race = new Race
            {
                RaceName = request.RaceName,
                IdRaceType = request.IdRaceType,
                Kilometer = request.Kilometer,
                Location = request.Location,
                Date = request.Date,
                Description = request.Description,
                NumberPlace = request.NumberPlace,
                Image = imagePath,
                Price = request.Price
            };

            var createdRace = await _raceRepository.AddAsync(race);

            _logger.LogInformation(
                "Race created successfully: {RaceName} (Id: {RaceId}) by User {UserId} ({Email})",
                createdRace.RaceName,
                createdRace.IdRace,
                userId,
                email
            );

            return new RaceDto(createdRace);
        }
    }
}