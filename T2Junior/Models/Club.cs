using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Club
{
    public int IdClub { get; set; }

    public string Name { get; set; } = null!;

    public string Rules { get; set; } = null!;

    public string Target { get; set; } = null!;

    public int Raiting { get; set; }

    public string? Reports { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
