using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Wall
{
    public int Id { get; set; }

    [Required]
    public int IdType { get; set; }

    [Required]
    public int IdOwner { get; set; }

    public virtual ApplicationUser Owner { get; set; } = null!;

    public virtual Club IdOwnerNavigation { get; set; } = null!;

    public virtual WallType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
