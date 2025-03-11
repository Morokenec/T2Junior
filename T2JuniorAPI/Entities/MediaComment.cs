using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class MediaComment : BaseCommonProperties
    {
        [Required]
        public required Guid IdComment{ get; set; }

        [Required]
        public required Guid IdMedia { get; set; }

        public virtual Mediafile IdMediaNavigation { get; set; } = null!;

        public virtual Comment IdCommentNavigation { get; set; } = null!;
    }
}
