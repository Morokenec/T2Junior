using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NoteStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteStatus>>> GetNoteStatuses()
        {
            return await _context.NoteStatuses.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteStatus(Guid id, NoteStatus noteStatus)
        {
            if (id != noteStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(noteStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<NoteStatus>> PostNoteStatus(NoteStatus noteStatus)
        {
            _context.NoteStatuses.Add(noteStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteStatus", new { id = noteStatus.Id }, noteStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteStatus(Guid id)
        {
            var noteStatus = await _context.NoteStatuses.FindAsync(id);
            if (noteStatus == null)
            {
                return NotFound();
            }

            _context.NoteStatuses.Remove(noteStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteStatusExists(Guid id)
        {
            return _context.NoteStatuses.Any(e => e.Id == id);
        }
    }
}
