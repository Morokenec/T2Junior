using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class ClubUser
{
    public int IdClub { get; set; }

    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual ClubRole IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
