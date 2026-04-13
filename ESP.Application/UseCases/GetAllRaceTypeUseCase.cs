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
    public class GetAllRaceTypeUseCase : IGetAllRaceTypeUseCase
    {
        private readonly IRaceTypeRepository _raceTypeRepository;
        public GetAllRaceTypeUseCase(IRaceTypeRepository raceTypeRepository)
        {
            _raceTypeRepository = raceTypeRepository;
        }
        public async Task<IList<RaceTypeDto>> Execute()
        {
            var types = await _raceTypeRepository.GetAllAsync();
            return types.Select(x => new RaceTypeDto(x)).ToList();
        }
    }
}