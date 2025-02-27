namespace T2JuniorAPI.Entities
{
    public class MediaInitiative : BaseCommonProperties
    {
        public Guid IdInitiative { get; set; }
        public Guid IdMedia { get; set; }

        public virtual Initiative Initiative { get; set; }
        public virtual Mediafile Mediafile { get; set; }
    }
}
