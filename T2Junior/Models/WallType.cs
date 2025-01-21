using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class WallType
{
    public int IdType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
