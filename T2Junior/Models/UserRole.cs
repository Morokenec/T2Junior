using System;
using System.Collections.Generic;

namespace T2Junior.Models;

public partial class UserRole
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
