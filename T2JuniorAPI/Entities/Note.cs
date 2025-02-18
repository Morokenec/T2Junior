using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class Note : BaseCommonProperties
{
    [Required]
    public required Guid IdWall { get; set; }

    [Required]
    public required Guid IdStatus { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    public Guid? IdRepost { get; set; }

    [Required]
    public int LikeCount { get; set; } = 0;


    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Note? IdRepostNavigation { get; set; }

    public virtual NoteStatus IdStatusNavigation { get; set; } = null!;

    public virtual Wall IdWallNavigation { get; set; } = null!;

    public virtual ICollection<Note> InverseIdRepostNavigation { get; set; } = new List<Note>();

    public virtual ICollection<MediaNote> MediaNotes { get; set; } = new List<MediaNote>();
}
