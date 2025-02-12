using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Medias
{
    public interface IMediafileService
    {
        Task<string> DeleteMediaByUserId(MediafileDeleteDTO mediafileDeleteDTO);
        Task<Mediafile> AddMediaByUserId(MediafileUploadDTO uploadDTO);
        Task<IEnumerable<MediafileDTO>> GetAllMediaByUserId(Guid userId);
        Task<string> SetAvatarByUserId(MediafileUploadDTO uploadDTO);
        Task<string> DeleteAvatarByUser(MediafileDeleteDTO deleteDTO);
        Task<Mediafile> CreateMediafileAsync(MediafileUploadDTO uploadDTO, bool isAvatar = false);
    }
}
