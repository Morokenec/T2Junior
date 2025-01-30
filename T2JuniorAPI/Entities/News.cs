using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class News : BaseCommonProperties
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}