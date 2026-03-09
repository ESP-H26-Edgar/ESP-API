using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;

namespace ESP.Application.UseCases.Interface
{
    public interface IGetAllRaceUseCase
    {
        Task<IList<RaceDto>> Execute();
    }
}
