using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaComments;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaCommentsController : ControllerBase
    {
        private readonly IMediaCommentService _mediaCommentService;

        public MediaCommentsController(IMediaCommentService mediaCommentService)
        {
            _mediaCommentService = mediaCommentService;
        }

        /// <summary>
        /// Добавление медиафайла к комментарию по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Обновленный комментарий с добавленным медиафайлом.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add/{commentId}")]
        public async Task<ActionResult<CommentDTO>> AddMediaToComment(Guid commentId, [FromForm] MediafileUploadDTO uploadDTO)
        {
            var comment = await _mediaCommentService.AddMediaToComment(commentId, uploadDTO);
            return Ok(comment);
        }

        /// <summary>
        /// Удаление медиафайла из комментария по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="mediaId">Идентификатор медиафайла.</param>
        /// <returns>Результат удаления медиафайла.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete/{commentId}/{mediaId}")]
        public async Task<ActionResult<bool>> DeleteMediaFromComment(Guid commentId, Guid mediaId)
        {
            var result = await _mediaCommentService.DeleteMediaFromComment(commentId, mediaId);
            return Ok(result);
        }
    }
}
