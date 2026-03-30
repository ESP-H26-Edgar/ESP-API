using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Security
{

    public interface IStripeService
    {
        Task<string> CreatePaymentIntentAsync(int idRace, decimal price, string prenom, string nom, string adresseMail, string Phone, string sexe, DateOnly dateNaissance );
        Task<Dictionary<string, string>?> VerifyAndExtractAsync(string json, string signature);
    }
}
