﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; }

    public string? Post { get; set; }

    [Required]
    public int AccumulatedPoints { get; set; }

    public Guid OrganizationId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public Organization Organization { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<InitiativeComment> InitiativeComments { get; set; }

    public virtual ICollection<Wall>? Walls { get; set; }

    public ICollection<UserSubscribers> SubscribersAsUser { get; set; }
    public ICollection<UserSubscribers> SubscribersAsSubscriber { get; set; }

    public virtual ICollection<Mediafile> Mediafiles { get; set; }

    public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    public virtual ICollection<UserInitiative> UserInitiatives { get; set; }

    public virtual ICollection<UserAvatar> UserAvatars { get; set; }
    public virtual ICollection<Vote> Votes { get; set; }

    public virtual ICollection<NoteLike> Likes { get; set; } = new List<NoteLike>();
    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

}
