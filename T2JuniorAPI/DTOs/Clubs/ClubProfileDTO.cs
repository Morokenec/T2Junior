﻿using AutoMapper;

namespace T2JuniorAPI.DTOs.Clubs
{
    public class ClubProfileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Target { get; set; } = null!;
        public string Rules { get; set; } = null!;
        public string? AvatarPath { get; set; }
        public int UsersCount { get; set; }
        public bool IsUserSubscribed { get; set; }
        public Guid? UserClubRole { get; set; }
    }
}
