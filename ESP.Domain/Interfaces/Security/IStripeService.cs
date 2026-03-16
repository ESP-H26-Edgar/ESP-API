using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Security
{

    public interface IStripeService
    {
        Task<string> CreatePaymentIntentAsync(int idRace, int idUser, decimal price);
        Task<Dictionary<string, string>?> VerifyAndExtractAsync(string json, string signature);
    }
}
