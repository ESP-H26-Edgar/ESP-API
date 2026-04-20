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
                "DeleteRace started by User {UserId} ({Email}) for RaceId {RaceId}",
                userId,
                email,
                id
            );

            var race = await _raceRepository.GetByIdAsync(id);

            if (race == null)
            {
                _logger.LogWarning(
                    "DeleteRace failed: Race not found (RaceId {RaceId}) by User {UserId}",
                    id,
                    userId
                );

                throw new Exception("Race introuvable");
            }

            await _raceRepository.DeleteAsync(race);

            _logger.LogInformation(
                "Race deleted successfully: {RaceName} (Id: {RaceId}) by User {UserId} ({Email})",
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