using System.ComponentModel.DataAnnotations;
using T2JuniorAPI.DTOs.Users;

namespace T2JuniorAPI.DTOs.Clubs
{
    public class ClubPageDTO 
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Target { get; set; } = null!;
        public string? AvatarPath { get; set; }

        public List<SubscriberProfileDTO> Users { get; set; }
    }
}
