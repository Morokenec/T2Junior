using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class Club : BaseCommonProperties
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Rules { get; set; }

    [Required]
    public required string Target { get; set; }

    [Required]
    public required int Raiting { get; set; }

    public string? Reports { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
