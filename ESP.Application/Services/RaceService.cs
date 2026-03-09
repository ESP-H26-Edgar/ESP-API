using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Repositories;

namespace ESP.Application.Services
{
    public class RaceService : IRaceService
    {
        private readonly IRaceRepository _raceRepository;

        public RaceService(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IList<RaceDto>> GetAllRace()
        {
            var races = await _raceRepository.GetAllRaces();
            return races.Select(x => new RaceDto(x)).ToList();
        }
    }
}
