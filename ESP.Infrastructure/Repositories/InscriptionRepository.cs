using ESP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Infrastructure.Repositories
{
    public class InscriptionRepository : IInscriptionRepository
    {
        private readonly AppDbContext _context;

        public InscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Registration registration)
            => await _context.Registrations.AddAsync(registration);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        // Génère un numero de dossard unique pour cette course
        public async Task<int> GenerateBibNumberAsync(int idRace)
        {
            var max = await _context.Registrations
                .Where(r => r.IdRace == idRace)
                .MaxAsync(r => (int?)r.BibNumber) ?? 0;

            return max + 1;
        }
        public async Task<bool> AlreadyExistsAsync( int idRace, string nom, string prenom, DateOnly dateNaissance, string email)
        {
            return await _context.Registrations.AnyAsync(r =>
                r.IdRace == idRace &&
                r.Nom == nom &&
                r.Prenom == prenom &&
                r.DateNaissance == dateNaissance &&
                r.AdresseMail == email
            );
        }
        public async Task<IEnumerable<Registration>> GetAllAsync()
        => await _context.Registrations.ToListAsync();

        public async Task<IEnumerable<Registration>> GetByRaceAsync(int idRace)
            => await _context.Registrations
                .Where(r => r.IdRace == idRace)
                .ToListAsync();

        public async Task<int> CountByRaceAsync(int idRace)
        {
            return await _context.Registrations
                .CountAsync(r => r.IdRace == idRace);
        }

        public async Task<Race> GetRaceByIdAsync(int idRace)
        {
            return await _context.Races
                .FirstAsync(r => r.IdRace == idRace);
        }
    }

}
