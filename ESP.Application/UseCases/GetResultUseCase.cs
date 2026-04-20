using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using ESP.Domain.Interfaces.Repositories;

namespace ESP.Application.UseCases
{
    public class GetResultsByRaceUseCase : IGetResultsByRaceUseCase
    {
        private readonly IResultRepository _resultRepository;

        public GetResultsByRaceUseCase(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<IList<ResultDto>> Execute(int idRace)
        {
            var results = await _resultRepository.GetByRaceIdAsync(idRace);
            return results.Select(r => new ResultDto(r)).ToList();
        }
    }
}