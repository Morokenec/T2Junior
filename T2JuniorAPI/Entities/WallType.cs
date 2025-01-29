using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class WallType
{

    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
