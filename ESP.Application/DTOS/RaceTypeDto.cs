using ESP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.DTOS
{
    public class RaceTypeDto
    {
        public int IdRaceType { get; set; }
        public string Name { get; set; } = null!;

        public RaceTypeDto() { }
        public RaceTypeDto(Racetype raceType)
        {
            IdRaceType = raceType.IdRaceType;
            Name = raceType.RaceType1;
        }
    }
}