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

        public async Task<List<Race>> GetAllAsync()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _context.Races.FirstOrDefaultAsync(x => x.IdRace == id);
        }
        public async Task<Race> AddAsync(Race race)
        {
            _context.Races.Add(race);
            await _context.SaveChangesAsync();
            return race;
        }
        public async Task DeleteAsync(Race race)
        {
            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
        }
    }
}
