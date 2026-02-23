using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Security
{
    //Demande à l'ia comment récuperer le mot de passe hasher
    public interface IPasswordHasher
    {
        bool Verify(string password, string hash);
        string Hash(string password);
    }
}
