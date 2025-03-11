using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.MediaInitiatives
{
    public interface IMediaInitiativeService
    {
        Task<MediaInitiative> AddMediaToInitiativeAsync(Guid initiativeId, MediafileUploadDTO uploadDTO);
        Task<bool> DeleteMediaFromInitiativeAsync(Guid initiativeId, Guid mediaId);
        Task<IEnumerable<MediafileDTO>> GetAllMediaForInitiativeAsync(Guid initiativeId);
    }
}
