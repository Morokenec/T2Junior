using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.Services.Comments;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CommentDTO>> AddCommentByNoteId(Guid noteId, [FromBody] CreateCommentDTO commentDTO)
        {
            var comment = await _commentService.AddCommentByNoteId(noteId, commentDTO);
            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult<bool>> DeleteComment(Guid commentId, Guid userId)
        {
            var result = await _commentService.DeleteComment(commentId, userId);
            return Ok(result);
        }

        [HttpPut("{commentId}")]
        public async Task<ActionResult<CommentDTO>> UpdateCommentById(Guid commentId, Guid userId, [FromBody] UpdateCommentDTO commentDTO)
        {
            var comment = await _commentService.UpdateCommentById(commentId, commentDTO, userId);
            return Ok(comment);
        }

        [HttpPost("add-parent/{parentId}")]
        public async Task<ActionResult<CommentDTO>> AddParentComment(Guid parentId, [FromBody] CreateCommentDTO commentDTO)
        {
            var comment = await _commentService.AddParrentComment(parentId, commentDTO);
            return Ok(comment);
        }

        [HttpPost("toggle-like/{commentId}")]
        public async Task<ActionResult<CommentDTO>> ToggleLikeComment(Guid commentId, Guid userId)
        {
            var comment = await _commentService.ToggleLikeComment(commentId, userId);
            return Ok(comment);
        }
    }
}
