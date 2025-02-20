using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Medias;

namespace T2JuniorAPI.Services.MediaComments
{
    public interface IMediaCommentService
    {
        Task<CommentDTO> AddMediaToComment(Guid commentId, MediafileUploadDTO uploadDTO);
        Task<bool> DeleteMediaFromComment(Guid commentId, Guid mediaId);
    }
}
