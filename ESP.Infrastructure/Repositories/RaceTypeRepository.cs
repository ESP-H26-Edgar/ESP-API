using ESP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Infrastructure.Repositories
{
    public class RaceTypeRepository : IRaceTypeRepository
    {
        private readonly AppDbContext _context;
        public RaceTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Racetype>> GetAllAsync()
        {
            return await _context.Racetypes.ToListAsync();
        }
    }
}