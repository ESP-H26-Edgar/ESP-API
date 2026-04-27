using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class RegisterDto
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public bool gender { get; set; }
        public DateOnly birthDate { get; set; }
        public string nationality { get; set; }
        public string? clubTeam { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
    }
}