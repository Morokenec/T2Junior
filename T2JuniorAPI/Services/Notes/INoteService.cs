using T2JuniorAPI.DTOs.Notes;

namespace T2JuniorAPI.Services.Notes
{
    public interface INoteService
    {
        Task<NoteDTO> CreateNoteAsync(Guid idOwner, CreateNoteDTO noteDTO);
        Task<NoteDTO> UpdateNoteAsync(Guid idNote, UpdateNoteDTO noteDTO);
        Task<bool> DeleteNoteAsync(Guid idNote);
        Task<NoteDTO> RepostNoteAsync(Guid idNote);
        Task<IEnumerable<NoteDTO>> GetNotesByIdOwnerAsync(Guid idOwner);
        Task<bool> UpdateNoteStatusAsync(Guid idNote, Guid idStatus);
    }
}
