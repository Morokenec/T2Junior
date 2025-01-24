using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public int OrganizationId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    [Required]
    public string? Post { get; set; }

    [Required]
    public DateTime? Birthday { get; set; }

    [Required]
    public bool Sex { get; set; }

    [Required]
    public int AccumulatedPoints { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual UserRole Role { get; set; } = null!;

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();

    public virtual ICollection<User> Subscribers { get; set; } = new List<User>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
