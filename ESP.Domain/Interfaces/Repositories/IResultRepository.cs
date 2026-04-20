using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Repositories
{
    public interface IResultRepository
    {
        Task<List<Result>> GetByRaceIdAsync(int idRace);
    }
}
