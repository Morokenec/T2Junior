namespace T2JuniorAPI.DTOs.Medias
{
    public class MediafileUploadDTO
    {
        public IFormFile File { get; set; }
        public Guid IdUser { get; set; }
    }
}
