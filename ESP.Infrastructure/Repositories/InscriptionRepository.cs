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
        private readonly AppDbContext _db;

        public InscriptionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Registration registration)
            => await _db.Registrations.AddAsync(registration);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();

        // Génère un numero de dossard unique pour cette course
        public async Task<int> GenerateBibNumberAsync(int idRace)
        {
            var max = await _db.Registrations
                .Where(r => r.IdRace == idRace)
                .MaxAsync(r => (int?)r.BibNumber) ?? 0;

            return max + 1;
        }
        public async Task<bool> AlreadyExistsAsync( int idRace, string nom, string prenom, DateOnly dateNaissance, string email)
        {
            return await _db.Registrations.AnyAsync(r =>
                r.IdRace == idRace &&
                r.Nom == nom &&
                r.Prenom == prenom &&
                r.DateNaissance == dateNaissance &&
                r.AdresseMail == email
            );
        }
        public async Task<IEnumerable<Registration>> GetAllAsync()
        => await _db.Registrations.ToListAsync();

        public async Task<IEnumerable<Registration>> GetByRaceAsync(int idRace)
            => await _db.Registrations
                .Where(r => r.IdRace == idRace)
                .ToListAsync();
    }
}
