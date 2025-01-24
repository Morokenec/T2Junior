using System.ComponentModel.DataAnnotations;

public class News
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
}
