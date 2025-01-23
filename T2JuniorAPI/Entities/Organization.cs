using System.ComponentModel.DataAnnotations;

public class Organization
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }
}
