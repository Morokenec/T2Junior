using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public required string IdNote { get; set; }

    [Required]
    public required int IdUser { get; set; }

    [Required]
    public required DateTime CreationDatetime { get; set; }

    [Required]
    public required string Text { get; set; }

    public int? ParrentCommentId { get; set; }
    
    [Required]
    public required int LikeCount { get; set; }

    public virtual Note IdNoteNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Comment> InverseParrentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParrentComment { get; set; }
}
