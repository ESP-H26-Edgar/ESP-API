using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Repositories
{
    public interface IInscriptionRepository
    {
        Task AddAsync(Registration registration);
        Task SaveChangesAsync();
        Task<bool> AlreadyExistsAsync(int idUser, int idRace, string nom, string prenom, DateOnly dateNaissance, string adresseMail);
        Task<int> GenerateBibNumberAsync(int idUser);
    }
}
