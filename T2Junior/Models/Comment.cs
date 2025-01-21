using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Comment
{
    public int IdComment { get; set; }

    public int IdNote { get; set; }

    public int IdUser { get; set; }

    public DateTime CreationDatetime { get; set; }

    public string Text { get; set; } = null!;

    public int? ParrentCommentId { get; set; }

    public int? LikeCount { get; set; }

    public virtual Note IdNoteNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Comment> InverseParrentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParrentComment { get; set; }
}
