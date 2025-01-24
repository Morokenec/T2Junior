using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Event
{
    public int Id { get; set; }

    [Required]
    public int IdClub { get; set; }

    [Required]
    public int IdDirection { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Place { get; set; } = null!;

    [Required]
    public DateTime Datetime { get; set; }

    [Required]
    public int NumberParticpants { get; set; }

    [Required]
    public int? FactParticpants { get; set; }

    public int Raiting { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual EventDirection IdDirectionNavigation { get; set; } = null!;
}
