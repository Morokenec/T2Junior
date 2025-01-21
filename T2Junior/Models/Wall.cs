using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Wall
{
    public int IdWall { get; set; }

    public int IdType { get; set; }

    public int IdOwner { get; set; }

    public virtual User IdOwner1 { get; set; } = null!;

    public virtual Club IdOwnerNavigation { get; set; } = null!;

    public virtual WallType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
