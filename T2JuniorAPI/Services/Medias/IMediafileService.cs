using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Medias
{
    public interface IMediafileService
    {
        Task<Mediafile> UploadMediafileAsync(MediafileUploadDTO uploadDTO);
    }
}
