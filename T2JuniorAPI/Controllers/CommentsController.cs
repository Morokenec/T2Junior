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

        /// <summary>
        /// Добавление комментария к заметке по её идентификатору.
        /// </summary>
        /// <param name="noteId">Идентификатор заметки.</param>
        /// <param name="commentDTO">DTO с данными для создания комментария.</param>
        /// <returns>Созданный комментарий.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add")]
        public async Task<ActionResult<CommentDTO>> AddCommentByNoteId(Guid noteId, [FromBody] CreateCommentDTO commentDTO)
        {
            var comment = await _commentService.AddCommentByNoteId(noteId, commentDTO);
            return Ok(comment);
        }

        /// <summary>
        /// Удаление комментария по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="userId">Идентификатор пользователя, удаляющего комментарий.</param>
        /// <returns>Результат удаления комментария.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult<bool>> DeleteComment(Guid commentId, Guid userId)
        {
            var result = await _commentService.DeleteComment(commentId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Обновление комментария по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="userId">Идентификатор пользователя, обновляющего комментарий.</param>
        /// <param name="commentDTO">DTO с обновленными данными комментария.</param>
        /// <returns>Обновленный комментарий.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut("{commentId}")]
        public async Task<ActionResult<CommentDTO>> UpdateCommentById(Guid commentId, Guid userId, [FromBody] UpdateCommentDTO commentDTO)
        {
            var comment = await _commentService.UpdateCommentById(commentId, commentDTO, userId);
            return Ok(comment);
        }

        /// <summary>
        /// Добавление родительского комментария к существующему комментарию.
        /// </summary>
        /// <param name="parentId">Идентификатор родительского комментария.</param>
        /// <param name="commentDTO">DTO с данными для создания комментария.</param>
        /// <returns>Созданный родительский комментарий.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add-parent/{parentId}")]
        public async Task<ActionResult<CommentDTO>> AddParentComment(Guid parentId, [FromBody] CreateCommentDTO commentDTO)
        {
            var comment = await _commentService.AddParrentComment(parentId, commentDTO);
            return Ok(comment);
        }

        /// <summary>
        /// Переключатель лайка в комментариях.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="userId">Идентификатор пользователя, ставящего лайк.</param>
        /// <returns>Комментарий с измененным статусом лайка.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("toggle-like/{commentId}")]
        public async Task<ActionResult<CommentDTO>> ToggleLikeComment(Guid commentId, Guid userId)
        {
            var comment = await _commentService.ToggleLikeComment(commentId, userId);
            return Ok(comment);
        }
    }
}
