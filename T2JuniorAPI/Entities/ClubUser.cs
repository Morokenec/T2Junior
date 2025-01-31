using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class ClubUser : BaseCommonProperties
{
    [Required]
    public required Guid IdClub { get; set; }

    [Required]
    public required Guid IdUser { get; set; }

    [Required]
    public required Guid IdRole { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual ClubRole IdRoleNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
