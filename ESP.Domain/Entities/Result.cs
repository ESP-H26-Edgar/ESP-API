using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Result
{
    public int IdResult { get; set; }

    public int IdRegistration { get; set; }

    public int IdRace { get; set; }

    public int Place { get; set; }

    public virtual Race IdRaceNavigation { get; set; } = null!;
    public virtual Registration IdRegistrationNavigation { get; set; } = null!;

}
