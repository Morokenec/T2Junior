using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Organization { get; set; }

    public string? Post { get; set; }

    public int? Age { get; set; }

    public sbyte Sex { get; set; }

    public int AccumulatedPoints { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual UserRole IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();

    public virtual ICollection<User> IdSubscribers { get; set; } = new List<User>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
