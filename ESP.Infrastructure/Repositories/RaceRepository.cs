using ESP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESP.Infrastructure.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly AppDbContext _context;

        public RaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Race>> GetAllRaces()
        {
            return await _context.Races.ToListAsync();
        }
    }
}
