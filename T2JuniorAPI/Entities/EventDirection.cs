using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class EventDirection
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
