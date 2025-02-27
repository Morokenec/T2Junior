using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Initiatives;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiativeService _initiativeService;

        public InitiativeController(IInitiativeService initiativeService)
        {
            _initiativeService = initiativeService;
        }

        [HttpPost]
        public async Task<ActionResult<Initiative>> CreateInitiative(InitiativeDTO initiativeDto)
        {
            var initiative = await _initiativeService.CreateInitiativeAsync(initiativeDto);
            return CreatedAtAction(nameof(GetInitiative), new { id = initiative.Id }, initiative);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Initiative>> GetInitiative(Guid id)
        {
            var initiative = await _initiativeService.GetInitiativeByIdAsync(id);
            if (initiative == null) return NotFound();
            return Ok(initiative);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Initiative>>> GetAllInitiatives()
        {
            var initiatives = await _initiativeService.GetAllInitiativesAsync();
            return Ok(initiatives);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInitiative(Guid id, InitiativeDTO initiativeDto)
        {
            var initiative = await _initiativeService.UpdateInitiativeAsync(id, initiativeDto);
            if (initiative == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInitiative(Guid id)
        {
            var result = await _initiativeService.DeleteInitiativeAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/vote")]
        public async Task<IActionResult> VoteForInitiative(Guid id)
        {
            var userId = Guid.Parse(User.Identity.Name); // Предполагается, что пользователь аутентифицирован
            var result = await _initiativeService.VoteForInitiativeAsync(id, userId);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> CommentOnInitiative(Guid id, InitiativeCommentDTO commentDto)
        {
            var result = await _initiativeService.CommentOnInitiativeAsync(id, commentDto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeInitiativeStatus(Guid id, [FromQuery] Guid statusId)
        {
            var result = await _initiativeService.ChangeInitiativeStatusAsync(id, statusId);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
