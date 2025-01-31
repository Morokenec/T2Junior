using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class WallType : BaseCommonProperties
{
    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
