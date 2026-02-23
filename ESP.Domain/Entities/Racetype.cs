using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Racetype
{
    public int IdRaceType { get; set; }

    public string RaceType1 { get; set; } = null!;

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
