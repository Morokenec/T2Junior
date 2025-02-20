using T2JuniorAPI.DTOs.Comments;

namespace T2JuniorAPI.Services.Comments
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentByNoteId(Guid noteId, CreateCommentDTO commentDTO);
        Task<bool> DeleteComment(Guid commentId, Guid userId);
        Task<CommentDTO> UpdateCommentById(Guid commentId, UpdateCommentDTO commentDTO, Guid userId);
        Task<CommentDTO> AddParrentComment(Guid parrentId, CreateCommentDTO commentDTO);
        Task<CommentDTO> ToggleLikeComment(Guid commentId, Guid userId);

    }
}
