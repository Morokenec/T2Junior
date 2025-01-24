using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.Models;

public class Organization
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}
