namespace T2JuniorAPI.DTOs.Medias
{
    public class MediafileDTO
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdType { get; set; }
        public string Path { get; set; }
        public string TypeName { get; set; }
    }
}
