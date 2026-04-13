using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Infrastructure;

namespace ESP.Domain.Interfaces.Repositories
{
    public interface IRaceRepository
    {
        Task<List<Race>> GetAllAsync();
        Task<Race?> GetByIdAsync(int id);
        Task<Race> AddAsync(Race race);
        Task DeleteAsync(Race race);
    }
}

