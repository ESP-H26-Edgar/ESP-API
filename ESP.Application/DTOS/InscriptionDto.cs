using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class InscriptionDto
    {
        public int IdRace { get; set; }
        public int IdUser { get; set; }
        public decimal Price { get; set; }

        public string Prenom { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string AdresseMail { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Sexe { get; set; } = null!;
        public DateOnly DateNaissance { get; set; }
    }
}
