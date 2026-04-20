using ESP.Application.DTOS;
using ESP.Application.Services;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;
using ESP.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class CreateRaceUseCase : ICreateRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;
        private readonly ImageService _imageService;
        private readonly ILogger<CreateRaceUseCase> _logger;

        public CreateRaceUseCase(
            IRaceRepository raceRepository,
            ImageService imageService,
            ILogger<CreateRaceUseCase> logger)
        {
            _raceRepository = raceRepository;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<RaceDto> Execute(CreateRaceRequest request)
        {
            _logger.LogInformation("Starting race creation: {RaceName}", request.RaceName);

            if (string.IsNullOrWhiteSpace(request.RaceName))
            {
                _logger.LogWarning("Race creation failed: Race name is missing");
                throw new ArgumentException("Race name is required");
            }

            if (request.Kilometer <= 0)
            {
                _logger.LogWarning("Race creation failed: invalid Kilometer value {Kilometer}", request.Kilometer);
                throw new ArgumentException("Kilometer must be greater than 0");
            }

            if (request.NumberPlace <= 0)
            {
                _logger.LogWarning("Race creation failed: invalid NumberPlace value {NumberPlace}", request.NumberPlace);
                throw new ArgumentException("NumberPlace must be greater than 0");
            }

            if (request.Price < 0)
            {
                _logger.LogWarning("Race creation failed: negative price {Price}", request.Price);
                throw new ArgumentException("Price cannot be negative");
            }

            var imagePath = await _imageService.SaveImage(request.Image);

            _logger.LogInformation("Image saved successfully: {ImagePath}", imagePath);

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

            _logger.LogInformation("Race created successfully with ID {RaceId}", createdRace.IdRace);

            return new RaceDto(createdRace);
        }
    }
}