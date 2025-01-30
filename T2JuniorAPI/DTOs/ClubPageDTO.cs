using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.DTOs
{
    public class ClubPageDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        
        public string Target { get; set; } = null!;

        public List<SubscriberProfileDTO> Users { get; set; } 
    }
}
