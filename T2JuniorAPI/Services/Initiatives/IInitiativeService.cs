using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Initiatives
{
    public interface IInitiativeService
    {
        Task<InitiativeOutputDTO> CreateInitiativeAsync(InitiativeInputDTO initiativeDto);
        Task<InitiativeOutputDTO> UpdateInitiativeAsync(Guid id, InitiativeInputDTO initiativeDto);
        Task<bool> DeleteInitiativeAsync(Guid id);
        Task<IEnumerable<InitiativeOutputDTO>> GetAllInitiativesWithDetailsAsync();
        Task<InitiativeOutputDTO> GetInitiativeByIdAsync(Guid id);
        Task<bool> VoteForInitiativeAsync(Guid id, Guid userId);
        Task<bool> CommentOnInitiativeAsync(Guid id, CreateInitiativeComment commentDto);
        Task<bool> ChangeInitiativeStatusAsync(Guid id, Guid statusId);
        Task<bool> AddUserToInitiativeAsync(Guid initiativeId, Guid userId);
        Task<bool> RemoveUserFromInitiativeAsync(Guid initiativeId, Guid userId);
        Task<IEnumerable<InitiativeStatusDTO>> GetInitiativeStatuses();
    }
}
