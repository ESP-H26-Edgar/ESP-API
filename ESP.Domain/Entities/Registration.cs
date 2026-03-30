using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class Registration
{
    public int IdUser { get; set; }

    public int IdRace { get; set; }

    public int BibNumber { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string AdresseMail { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string Sexe { get; set; } = null!;
    public DateOnly DateNaissance { get; set; }

    public virtual Race IdRaceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
}
