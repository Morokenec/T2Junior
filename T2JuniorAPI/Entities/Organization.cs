using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.Entities;

public class Organization : BaseCommonProperties
{
    [Required]
    public required string Name { get; set; }

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
