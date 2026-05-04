using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ESP.Application.UseCases
{
    public class DeleteRaceUseCase : IDeleteRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;
        private readonly ILogger<DeleteRaceUseCase> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteRaceUseCase(
            IRaceRepository raceRepository,
            ILogger<DeleteRaceUseCase> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RaceDto> ExecuteAsync(int id)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userId = user?.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;
            var email = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            _logger.LogInformation(
                "Suppresion en cours par {UserId} ({Email}) pour {RaceId}",
                userId,
                email,
                id
            );

            var race = await _raceRepository.GetByIdAsync(id);

            if (race == null)
            {
                _logger.LogWarning(
                    "Course non trouvée : (RaceId {RaceId}) par {UserId}",
                    id,
                    userId
                );

                throw new Exception("Race introuvable");
            }

            await _raceRepository.DeleteAsync(race);

            _logger.LogInformation(
                "Suppresion avec succes de {RaceName} (Id: {RaceId})  par {UserId} ({Email})",
                race.RaceName,
                race.IdRace,
                userId,
                email
            );

            return new RaceDto
            {
                IdRace = race.IdRace,
                RaceName = race.RaceName,
                Date = race.Date
            };
        }
    }
}