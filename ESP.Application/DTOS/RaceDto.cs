using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Infrastructure;

namespace ESP.Application.DTOS
{
    public class RaceDto
    {
        public int IdRace { get; set; }

        public string RaceName { get; set; } = null!;

        public int IdRaceType { get; set; }

        public int Kilometer { get; set; }

        public string Location { get; set; } = null!;

        public DateOnly Date { get; set; }

        public string Description { get; set; } = null!;

        public int NumberPlace { get; set; }

        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public RaceDto() { }

        public RaceDto(Race race)
        {
            IdRace = race.IdRace;
            RaceName = race.RaceName;
            IdRaceType = race.IdRaceType;
            Kilometer = race.Kilometer;
            Location = race.Location;
            Date = race.Date;
            Description = race.Description;
            NumberPlace = race.NumberPlace;
            Image = race.Image;
            Price = race.Price;
        }
    }
}
