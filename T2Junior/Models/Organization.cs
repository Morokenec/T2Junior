using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class Organization
{
    public int IdOrganization { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
