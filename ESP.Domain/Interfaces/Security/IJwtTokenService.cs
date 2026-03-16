using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(string mail, bool isAdmin, int idUser);
    }
}
