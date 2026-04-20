using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class ResultDto
    {
        public int IdResult { get; set; }
        public int IdRegistration { get; set; }
        public int IdRace { get; set; }
        public int Place { get; set; }
        public int BibNumber { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;

        public ResultDto(Result result)
        {
            IdResult = result.IdResult;
            IdRegistration = result.IdRegistration;
            IdRace = result.IdRace;
            Place = result.Place;
            BibNumber = result.IdRegistrationNavigation?.BibNumber ?? 0;
            Nom = result.IdRegistrationNavigation?.Nom ?? "—";
            Prenom = result.IdRegistrationNavigation?.Prenom ?? "—";
        }
    }