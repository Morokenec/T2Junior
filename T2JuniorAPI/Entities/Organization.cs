using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.Models;

public class Organization
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
