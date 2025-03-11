using T2JuniorAPI.DTOs.Notes;

namespace T2JuniorAPI.Services.NoteStatuses
{
    public interface INoteStatusService
    {
        Task<NoteStatusDTO> GetOrCreateNoteStatusAsync(string name);
        Task<NoteStatusDTO> UpdateNoteStatusAsync(NoteStatusDTO noteStatusDTO);
        Task<bool> DeleteNoteStatusAsync(Guid id);
        Task<IEnumerable<NoteStatusDTO>> GetAllNoteStatusesAsync();
    }
}
