using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class ClubRole
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();
}
