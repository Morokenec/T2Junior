using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class ClubUser
{
    public int IdClub { get; set; }

    [Required]
    public int IdUser { get; set; }

    [Required]
    public int IdRole { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual ClubRole IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
