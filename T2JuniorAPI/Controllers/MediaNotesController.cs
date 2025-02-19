using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaNotes;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaNotesController : ControllerBase
    {
        private readonly IMediaNoteService _mediaNoteService;

        public MediaNotesController(IMediaNoteService mediaNoteService)
        {
            _mediaNoteService = mediaNoteService;
        }

        [HttpPost("add-media-to-note/{idNote}")]
        public async Task<ActionResult<MediaNoteDTO>> AddMediaToNote(Guid idNote, MediafileUploadDTO uploadDTO)
        {
            var mediaNote = await _mediaNoteService.AddMediaToNoteAsync(idNote, uploadDTO);
            return Ok(mediaNote);
        }

        [HttpDelete("delete-media-from-note/{idNote}/{idMedia}")]
        public async Task<ActionResult<bool>> DeleteMediaFromNote(Guid idNote, Guid idMedia)
        {
            var result = await _mediaNoteService.DeleteMediaFromNoteAsync(idNote, idMedia);
            return Ok(result);
        }
    }
}
