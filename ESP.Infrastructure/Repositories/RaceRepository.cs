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
        public async Task<Race> AddAsync(Race race)
        {
            _context.Races.Add(race);

            // LOG TEMPORAIRE - à supprimer après
            var sql = _context.Database.GenerateCreateScript();
            Console.WriteLine("=== SCHEMA SQL ===");
            Console.WriteLine(sql);
            Console.WriteLine("=== PRICE VALUE ===");
            Console.WriteLine($"Price: {race.Price}");

            await _context.SaveChangesAsync();
            return race;
        }
    }
}
