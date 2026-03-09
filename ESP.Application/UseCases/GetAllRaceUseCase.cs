using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;

namespace ESP.Application.UseCases
{
    public class GetAllRaceUseCase : IGetAllRaceUseCase
    {
        private readonly IRaceRepository _raceRepository;

        public GetAllRaceUseCase(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<IList<RaceDto>> Execute()
        {
            var todos = await _raceRepository.GetAllRaces();
            return todos.Select(x => new RaceDto(x)).ToList();
        }
    }
}
