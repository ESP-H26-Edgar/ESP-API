using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Result
{
    public int IdResult { get; set; }

    public int IdUser { get; set; }

    public int IdRace { get; set; }

    public int Place { get; set; }

    public virtual Race IdRaceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
