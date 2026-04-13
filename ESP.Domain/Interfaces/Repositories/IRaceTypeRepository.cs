using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Domain.Interfaces.Repositories
{

   
        public interface IRaceTypeRepository
        {
            Task<List<Racetype>> GetAllAsync();
        }
    }
