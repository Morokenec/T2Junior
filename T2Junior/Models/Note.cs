using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Note
{
    public int IdNote { get; set; }

    public int IdWall { get; set; }

    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreationDatetime { get; set; }

    public int? IdRepost { get; set; }

    public int LikeCount { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Note? IdRepostNavigation { get; set; }

    public virtual NoteStatus IdStatusNavigation { get; set; } = null!;

    public virtual Wall IdWallNavigation { get; set; } = null!;

    public virtual ICollection<Note> InverseIdRepostNavigation { get; set; } = new List<Note>();
}
