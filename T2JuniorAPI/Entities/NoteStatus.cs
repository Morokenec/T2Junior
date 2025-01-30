using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class NoteStatus : BaseCommonProperties
{
    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
