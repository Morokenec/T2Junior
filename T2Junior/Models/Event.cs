using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Event
{
    public int IdEvent { get; set; }

    public int IdClub { get; set; }

    public int IdDirection { get; set; }

    public string Name { get; set; } = null!;

    public string Place { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public int NumberParticpants { get; set; }

    public int? FactParticpants { get; set; }

    public int Raiting { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual EventDirection IdDirectionNavigation { get; set; } = null!;
}
