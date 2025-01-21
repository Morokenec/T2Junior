using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class NoteStatus
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
