using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    [Required]
    public  string PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; }

    public string? Post { get; set; }

    [Required]
    public DateTime? Birthday { get; set; }

    [Required]
    public int AccumulatedPoints { get; set; }

    public int OrganizationId { get; set; }

    public Organization Organization { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();

    public virtual ICollection<ApplicationUser> Subscribers { get; set; } = new List<ApplicationUser>();
}
