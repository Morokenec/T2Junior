using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class MediaType : BaseCommonProperties
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Mediafile> Mediafiles { get; set; } = null!;
    }
}
