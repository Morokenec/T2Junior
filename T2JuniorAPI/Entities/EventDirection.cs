using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class EventDirection : BaseCommonProperties
{
    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
