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
    public class GetRaceByIdUseCase : IGetRaceByIdUseCase
    {
        private readonly IRaceRepository _raceRepository;

        public GetRaceByIdUseCase(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<RaceDto?> ExecuteAsync(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);

            if (race == null)
                return null;

            return new RaceDto
            {
                IdRace = race.IdRace,
                RaceName = race.RaceName,
                IdRaceType = race.IdRaceType,
                Kilometer = race.Kilometer,
                Location = race.Location,
                Date = race.Date,
                Description = race.Description,
                NumberPlace = race.NumberPlace,
                Image = race.Image,
                Price = race.Price
            };
        }
    }
}
