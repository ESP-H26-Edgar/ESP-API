using ESP.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.UseCases.Interface
{
    public interface ICreateRaceUseCase
    {
        Task<RaceDto> Execute(CreateRaceRequest request);
    }
}
