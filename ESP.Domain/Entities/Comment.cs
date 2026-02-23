using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Comment
{
    public int IdComment { get; set; }

    public int IdRace { get; set; }

    public int Rate { get; set; }

    public string? Comment1 { get; set; }

    public virtual Race IdRaceNavigation { get; set; } = null!;
}
