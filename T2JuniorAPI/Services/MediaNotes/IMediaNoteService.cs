using T2JuniorAPI.DTOs.Medias;

namespace T2JuniorAPI.Services.MediaNotes
{
    public interface IMediaNoteService
    {
        Task<MediaNoteDTO> AddMediaToNoteAsync(Guid idNote, MediafileUploadDTO uploadDTO);
        Task<bool> DeleteMediaFromNoteAsync(Guid idNote, Guid idMedia);
    }
}
