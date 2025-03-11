using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.NoteStatuses;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteStatusController : ControllerBase
    {
        private readonly INoteStatusService _noteStatusService;

        public NoteStatusController(INoteStatusService noteStatusService)
        {
            _noteStatusService = noteStatusService;
        }

        [HttpPost("get-or-create/{statusName}")]
        public async Task<ActionResult> GetOrCreateNoteStatus(string statusName)
        {
            var noteStatus = await _noteStatusService.GetOrCreateNoteStatusAsync(statusName);
            return Ok(noteStatus);
        }


        [HttpPut]
        public async Task<ActionResult<NoteStatusDTO>> UpdateNoteStatus([FromBody] NoteStatusDTO updateDto)
        {
            var noteStatus = await _noteStatusService.UpdateNoteStatusAsync(updateDto);
            return Ok(noteStatus);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteNoteStatus(Guid id)
        {
            var result = await _noteStatusService.DeleteNoteStatusAsync(id);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<NoteStatusDTO>>> GetAllNoteStatuses()
        {
            var noteStatuses = await _noteStatusService.GetAllNoteStatusesAsync();
            return Ok(noteStatuses);
        }
    }
}
