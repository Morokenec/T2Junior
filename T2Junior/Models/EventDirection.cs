using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class EventDirection
{
    public int IdDirection { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
