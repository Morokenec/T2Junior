﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class Comment : BaseCommonProperties
{
    [Required]
    public required Guid IdNote { get; set; }

    [Required]
    public required Guid IdUser { get; set; }

    [Required]
    public required string Text { get; set; }

    public Guid? ParrentCommentId { get; set; }
    
    [Required]
    public required int LikeCount { get; set; }

    public virtual Note IdNoteNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; }

    public virtual ICollection<Comment> InverseParrentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParrentComment { get; set; }
    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
    public virtual ICollection<MediaComment> MediaComments { get; set; } = new List<MediaComment>();

}
