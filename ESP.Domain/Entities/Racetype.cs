using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Infrastructure;

[Table("RaceTypes")]
public partial class Racetype
{
    public int IdRaceType { get; set; }

    [Column("RaceType")]
    public string RaceType1 { get; set; } = null!;

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
