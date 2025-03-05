using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Notes;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        /// Создание новой заметки для владельца.
        /// </summary>
        /// <param name="idOwner">Идентификатор владельца заметки.</param>
        /// <param name="noteDTO">DTO с данными для создания заметки.</param>
        /// <returns>Созданная заметка.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("create/{idOwner}")]
        public async Task<ActionResult<NoteDTO>> CreateNote(Guid idOwner, [FromBody] CreateNoteDTO noteDTO)
        {
            var note = await _noteService.CreateNoteAsync(idOwner, noteDTO);
            return Ok(note);
        }

        /// <summary>
        /// Обновление существующей заметки.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="noteDTO">DTO с обновленными данными заметки.</param>
        /// <returns>Обновленная заметка.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut("update/{idNote}")]
        public async Task<ActionResult<NoteDTO>> UpdateNote(Guid idNote, [FromBody] UpdateNoteDTO noteDTO)
        {
            var note = await _noteService.UpdateNoteAsync(idNote, noteDTO);
            return Ok(note);
        }

        /// <summary>
        /// Удаление заметки по её идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <returns>Результат удаления заметки.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{idNote}")]
        public async Task<ActionResult<bool>> DeleteNote(Guid idNote)
        {
            var result = await _noteService.DeleteNoteAsync(idNote);
            return Ok(result);
        }

        /// <summary>
        /// Репост заметки.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="idOwner">Идентификатор владельца, репостящего заметку.</param>
        /// <returns>Репостнутая заметка.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("repost/{idNote}/{idOwner}")]
        public async Task<ActionResult<NoteDTO>> RepostNote(Guid idNote, Guid idOwner)
        {
            var note = await _noteService.RepostNoteAsync(idNote, idOwner);
            return Ok(note);
        }

        /// <summary>
        /// Получение заметок по идентификатору владельца.
        /// </summary>
        /// <param name="idOwner">Идентификатор владельца заметок.</param>
        /// <returns>Список заметок владельца.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("get-by-id-owner/{idOwner}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotesByIdOwner(Guid idOwner)
        {
            var notes = await _noteService.GetNotesWithCommentCountAsync(idOwner);
            return Ok(notes);
        }

        /// <summary>
        /// Получение подкомментария к заметке.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <returns>Список подкомментариев к заметке.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("get-comments-by-id-note/{idNote}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetSubCommentsByParrent(Guid idNote)
        {
            var notes = await _noteService.GetCommentsWithSubCommentsAsync(idNote);
            return Ok(notes);
        }

        /// <summary>
        /// Обновление статуса заметки.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="idStatus">Идентификатор нового статуса.</param>
        /// <returns>Результат обновления статуса заметки.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPatch("update-status/{idNote}/{idStatus}")]
        public async Task<ActionResult<bool>> UpdateNoteStatus(Guid idNote, Guid idStatus)
        {
            var result = await _noteService.UpdateNoteStatusAsync(idNote, idStatus);
            return Ok(result);
        }

        /// <summary>
        /// Переключатель лайка в заметках.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="idUser">Идентификатор пользователя, ставящего лайк.</param>
        /// <returns>Обновленная заметка с измененным статусом лайка.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("toggle-like/{idNote}")]
        public async Task<ActionResult<NoteDTO>> ToggleLikeNole(Guid idNote, Guid idUser)
        {
            var note = await _noteService.ToggleLikeNoteAsync(idNote, idUser);
            return Ok(note);
        }
    }
}
