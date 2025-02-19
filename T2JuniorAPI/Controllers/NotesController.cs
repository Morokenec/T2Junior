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

        [HttpPost("create/{idOwner}")]
        public async Task<ActionResult<NoteDTO>> CreateNote(Guid idOwner, [FromBody] CreateNoteDTO noteDTO)
        {
            var note = await _noteService.CreateNoteAsync(idOwner, noteDTO);
            return Ok(note);
        }

        [HttpPut("update/{idNote}")]
        public async Task<ActionResult<NoteDTO>> UpdateNote(Guid idNote, [FromBody] UpdateNoteDTO noteDTO)
        {
            var note = await _noteService.UpdateNoteAsync(idNote, noteDTO);
            return Ok(note);
        }

        [HttpDelete("{idNote}")]
        public async Task<ActionResult<bool>> DeleteNote(Guid idNote)
        {
            var result = await _noteService.DeleteNoteAsync(idNote);
            return Ok(result);
        }

        [HttpPost("repost/{idNote}/{idOwner}")]
        public async Task<ActionResult<NoteDTO>> RepostNote(Guid idNote, Guid idOwner)
        {
            var note = await _noteService.RepostNoteAsync(idNote, idOwner);
            return Ok(note);
        }

        [HttpGet("get-by-id-owner/{idOwner}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotesByIdOwner(Guid idOwner)
        {
            var notes = await _noteService.GetNotesByIdOwnerAsync(idOwner);
            return Ok(notes);
        }

        [HttpPatch("update-status/{idNote}/{idStatus}")]
        public async Task<ActionResult<bool>> UpdateNoteStatus(Guid idNote, Guid idStatus)
        {
            var result = await _noteService.UpdateNoteStatusAsync(idNote, idStatus);
            return Ok(result);
        }

        [HttpPost("toggle-like/{idNote}")]
        public async Task<ActionResult<NoteDTO>> ToggleLikeNole(Guid idNote, Guid idUser)
        {
            var note = await _noteService.ToggleLikeNoteAsync(idNote, idUser);
            return Ok(note);
        }
    }
}
