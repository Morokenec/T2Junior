using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models;

public class Event
{
    public int Id { get; set; }

    [Required]
    public required int IdClub { get; set; }

    [Required]
    public required int IdDirection { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Place { get; set; }

    [Required]
    public required DateTime Datetime { get; set; }

    [Required]
    public required int NumberParticpants { get; set; }

    [Required]
    public required int? FactParticpants { get; set; }

    public int Raiting { get; set; }

    public virtual Club IdClubNavigation { get; set; } = null!;

    public virtual EventDirection IdDirectionNavigation { get; set; } = null!;
}
