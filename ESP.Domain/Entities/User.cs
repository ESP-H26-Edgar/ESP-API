using System;
using System.Collections.Generic;

namespace ESP.Infrastructure;

public partial class User
{
    public int IdUser { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public bool Gender { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Nationality { get; set; } = null!;

    public string? ClubTeam { get; set; }

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
