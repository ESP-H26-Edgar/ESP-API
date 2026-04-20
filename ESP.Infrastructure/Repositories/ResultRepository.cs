using ESP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESP.Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _context;

        public ResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Result>> GetByRaceIdAsync(int idRace)
        {
            return await _context.Results
                .Where(r => r.IdRace == idRace)
                .OrderBy(r => r.Place)
                .ToListAsync();
        }
    }
}