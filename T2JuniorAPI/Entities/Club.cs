using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Club
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Rules { get; set; } = null!;

    [Required]
    public string Target { get; set; } = null!;

    [Required]
    public int Raiting { get; set; }

    public string? Reports { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Wall> Walls { get; set; } = new List<Wall>();
}
