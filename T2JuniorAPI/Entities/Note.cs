using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Note
{
    public int Id { get; set; }

    [Required]
    public int IdWall { get; set; }

    [Required]
    public int IdStatus { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime CreationDatetime { get; set; }

    public int? IdRepost { get; set; }

    [Required]
    public int LikeCount { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Note? IdRepostNavigation { get; set; }

    public virtual NoteStatus IdStatusNavigation { get; set; } = null!;

    public virtual Wall IdWallNavigation { get; set; } = null!;

    public virtual ICollection<Note> InverseIdRepostNavigation { get; set; } = new List<Note>();
}
