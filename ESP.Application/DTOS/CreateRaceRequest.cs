using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class CreateRaceRequest
    {
        public string RaceName { get; set; } = null!;
        public int IdRaceType { get; set; }
        public double Kilometer { get; set; }
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int NumberPlace { get; set; }
        public IFormFile Image { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
