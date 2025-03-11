using T2JuniorAPI.DTOs.MediaTypes;

namespace T2JuniorAPI.Services.MediaTypes
{
    public interface IMediaTypeService
    {
        Task<MediaTypeDTO> GetGuidOrCreateMediaType(MediaTypeDTO mediaTypeDTO);
        Task<List<MediaTypeDTO>> GetAllMediaTypes();
        Task<bool> DeleteMediaType(Guid id);
    }
}
