using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Note
{
    public int Id { get; set; }

    [Required]
    public required int IdWall { get; set; }

    [Required]
    public required int IdStatus { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required DateTime CreationDatetime { get; set; }

    public int? IdRepost { get; set; }

    [Required]
    public required int LikeCount { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Note? IdRepostNavigation { get; set; }

    public virtual NoteStatus IdStatusNavigation { get; set; } = null!;

    public virtual Wall IdWallNavigation { get; set; } = null!;

    public virtual ICollection<Note> InverseIdRepostNavigation { get; set; } = new List<Note>();
}
