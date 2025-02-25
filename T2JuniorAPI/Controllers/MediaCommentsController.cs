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

        [HttpPost("add/{commentId}")]
        public async Task<ActionResult<CommentDTO>> AddMediaToComment(Guid commentId, [FromForm] MediafileUploadDTO uploadDTO)
        {
            var comment = await _mediaCommentService.AddMediaToComment(commentId, uploadDTO);
            return Ok(comment);
        }

        [HttpDelete("delete/{commentId}/{mediaId}")]
        public async Task<ActionResult<bool>> DeleteMediaFromComment(Guid commentId, Guid mediaId)
        {
            var result = await _mediaCommentService.DeleteMediaFromComment(commentId, mediaId);
            return Ok(result);
        }
    }
}
