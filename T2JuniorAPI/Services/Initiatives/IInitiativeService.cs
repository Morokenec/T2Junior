using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Initiatives
{
    public interface IInitiativeService
    {
        Task<Initiative> CreateInitiativeAsync(InitiativeDTO initiativeDto);
        Task<Initiative> UpdateInitiativeAsync(Guid id, InitiativeDTO initiativeDto);
        Task<bool> DeleteInitiativeAsync(Guid id);
        Task<IEnumerable<Initiative>> GetAllInitiativesAsync();
        Task<Initiative> GetInitiativeByIdAsync(Guid id);
        Task<bool> VoteForInitiativeAsync(Guid id, Guid userId);
        Task<bool> CommentOnInitiativeAsync(Guid id, InitiativeCommentDTO commentDto);
        Task<bool> ChangeInitiativeStatusAsync(Guid id, Guid statusId);
        Task<IEnumerable<Initiative>> GetInitiativesWithRatingAsync();
    }
}
