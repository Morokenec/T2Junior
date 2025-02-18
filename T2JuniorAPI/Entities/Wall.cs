using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class Wall : BaseCommonProperties
{
    [Required]
    public required Guid IdType { get; set; }

    [Required]
    public required Guid IdOwner { get; set; }

    public virtual ApplicationUser? UserOwner { get; set; }

    public virtual Club? ClubOwner { get; set; }

    public virtual WallType IdTypeNavigation { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
