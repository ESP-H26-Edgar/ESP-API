using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Race
{
    public int IdRace { get; set; }

    public string RaceName { get; set; } = null!;

    public int IdRaceType { get; set; }

    public float Kilometer { get; set; }

    public string Location { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public int NumberPlace { get; set; }

    public string Image { get; set; } = null!;
    public decimal Price { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Racetype IdRaceTypeNavigation { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
