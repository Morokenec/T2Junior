using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities;

public class Event : BaseCommonProperties
{
    [Required]
    public required Guid IdClub { get; set; }

    [Required]
    public required Guid IdDirection { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Place { get; set; }

    [Required]
    public required DateTime StartDatetime { get; set; }
    
    [Required]
    public required DateTime EndDatetime { get; set; }

    [Required]
    public required int NumberParticpants { get; set; }

    public int? FactParticpants { get; set; }

    public int Raiting { get; set; }

    public virtual Club IdClubNavigation { get; set; }

    public virtual EventDirection IdDirectionNavigation { get; set; }

    public virtual ICollection<MediaEvent> MediaEvents { get; set; }
}
