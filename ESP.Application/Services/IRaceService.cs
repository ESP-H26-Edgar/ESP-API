using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;

namespace ESP.Application.Services
{
    public interface IRaceService
    {
        public Task<IList<RaceDto>> GetAllRace();
    }
}
