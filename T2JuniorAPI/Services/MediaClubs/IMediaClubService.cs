using T2JuniorAPI.DTOs.Medias;

namespace T2JuniorAPI.Services.MediaClubs
{
    public interface IMediaClubService
    {
        Task<MediaClubDTO> AddMediaByClubId(MediafileUploadDTO uploadDTO, Guid clubId);
        Task<string> DeleteMediaByClubId(Guid clubId, Guid mediaId, Guid userId);
        Task<IEnumerable<MediaClubDTO>> GetAllMediaByClubId(Guid clubId);
        Task<string> SetAvatarForClub(MediafileUploadDTO uploadDTO, Guid clubId);
        Task<string> DeleteAvatarFromClub(Guid clubId, Guid mediaId, Guid userId);
    }
}
