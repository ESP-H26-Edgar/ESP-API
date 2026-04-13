using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class DeleteRaceUseCase : IDeleteRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;

        public DeleteRaceUseCase(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<RaceDto> ExecuteAsync(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);

            if (race == null)
                throw new Exception("Race introuvable");

            await _raceRepository.DeleteAsync(race);

            return new RaceDto
            {
                IdRace = race.IdRace,
                RaceName = race.RaceName,
                Date = race.Date
            };
        }
    }
}