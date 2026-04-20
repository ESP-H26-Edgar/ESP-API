using ESP.Application.DTOS;

namespace ESP.Application.UseCases.Interface
{
    public interface IGetResultsByRaceUseCase
    {
        Task<IList<ResultDto>> Execute(int idRace);
    }
}