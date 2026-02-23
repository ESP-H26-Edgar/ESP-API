using BCrypt.Net;
using ESP.Domain.Interfaces.Security;


namespace ESP.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    { 
        //demande à l'ia pour les fonctions de BCrypt 
        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
