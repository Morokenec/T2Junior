using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class ClubRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();
}
