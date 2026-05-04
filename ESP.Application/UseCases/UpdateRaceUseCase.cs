
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ESP.Application.DTOS;
using ESP.Application.Services;
using ESP.Domain.Interfaces.Repositories;
using ESP.Application.UseCases.Interface;

namespace ESP.Application.UseCases
    {
        public class UpdateRaceUseCase : IUpdateRaceUseCase
        {
            private readonly IRaceRepository _raceRepository;
            private readonly IImageService _imageService;
            private readonly ILogger<UpdateRaceUseCase> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateRaceUseCase(
                IRaceRepository raceRepository,
                IImageService imageService,
                ILogger<UpdateRaceUseCase> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _raceRepository = raceRepository;
                _imageService = imageService;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<RaceDto> Execute(UpdateRaceRequest request)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var userId = user?.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;
                var email = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                _logger.LogInformation(
                    "Mise a jour en cours par {UserId} ({Email}) pour {RaceId}",
                    userId, email, request.IdRace
                );

                var existing = await _raceRepository.GetByIdAsync(request.IdRace)
                    ?? throw new KeyNotFoundException($"Race {request.IdRace} not found");

                if (string.IsNullOrWhiteSpace(request.RaceName))
                {
                    _logger.LogWarning("Nom manquant par {UserId}", userId);
                    throw new ArgumentException("Race name is required");
                }

                if (request.Kilometer <= 0)
                {
                    _logger.LogWarning("Killometrage invalide ({Kilometer}) par {UserId}", request.Kilometer, userId);
                    throw new ArgumentException("Kilometer must be greater than 0");
                }

                if (request.NumberPlace <= 0)
                {
                    _logger.LogWarning("Nombre de place invalide ({NumberPlace}) par {UserId}", request.NumberPlace, userId);
                    throw new ArgumentException("NumberPlace must be greater than 0");
                }

                if (request.Price < 0)
                {
                    _logger.LogWarning("Prix invalide ({Price}) par {UserId}", request.Price, userId);
                    throw new ArgumentException("Price cannot be negative");
                }

                var imagePath = request.Image != null
                    ? await _imageService.SaveImage(request.Image)
                    : existing.Image;

                existing.RaceName = request.RaceName;
                existing.IdRaceType = request.IdRaceType;
                existing.Kilometer = request.Kilometer;
                existing.Location = request.Location;
                existing.Date = request.Date;
                existing.Description = request.Description;
                existing.NumberPlace = request.NumberPlace;
                existing.Image = imagePath;
                existing.Price = request.Price;

                var updatedRace = await _raceRepository.UpdateAsync(existing);

                _logger.LogInformation(
                    "Course mis à jour : {RaceName} (Id: {RaceId}) par {UserId} ({Email})",
                    updatedRace.RaceName, updatedRace.IdRace, userId, email
                );

                return new RaceDto(updatedRace);
            }
        }
}

