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

        /// <summary>
        /// Добавление медиафайла к заметке по её идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Обновленная заметка с добавленным медиафайлом.</returns>>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add-media-to-note/{idNote}")]
        public async Task<ActionResult<MediaNoteDTO>> AddMediaToNote(Guid idNote, MediafileUploadDTO uploadDTO)
        {
            var mediaNote = await _mediaNoteService.AddMediaToNoteAsync(idNote, uploadDTO);
            return Ok(mediaNote);
        }

        /// <summary>
        /// Удаление медиафайла из заметок по её идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="idMedia">Идентификатор медиафайла.</param>
        /// <returns>Результат удаления медиафайла.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete-media-from-note/{idNote}/{idMedia}")]
        public async Task<ActionResult<bool>> DeleteMediaFromNote(Guid idNote, Guid idMedia)
        {
            var result = await _mediaNoteService.DeleteMediaFromNoteAsync(idNote, idMedia);
            return Ok(result);
        }
    }
}
