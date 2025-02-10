using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.Services.Events;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("user-calendar")]
        public async Task<ActionResult<List<EventCalendarDTO>>> GetUserCalendar([FromQuery] Guid userId, [FromQuery] int year, [FromQuery] int month)
        {
            if (month < 1 || month > 12)
            {
                return BadRequest("Month value is not valid.");
            }

            if (year < 1)
            {
                return BadRequest("Year value is not valid.");
            }

            var events = await _eventService.GetUserCalendar(userId, month, year);
            return Ok(events);
        }

        [HttpGet("club-calendar")]
        public async Task<ActionResult<List<EventCalendarDTO>>> GetClubCalendar([FromQuery] Guid clubId, [FromQuery] int year, [FromQuery] int month)
        {
            if (month < 1 || month > 12)
            {
                return BadRequest("Month value is not valid.");
            }

            if (year < 1)
            {
                return BadRequest("Year value is not valid.");
            }

            var events = await _eventService.GetClubCalendar(clubId, month, year);
            return Ok(events);
        }

        [HttpPost("create-event")]
        public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventDTO createEventDTO)
        {
            try
            {
                var eventId = await _eventService.CreateEvent(createEventDTO);
                return Ok(eventId);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            await _eventService.DeleteEvent(eventId);
            return NoContent();
        }

        [HttpPut("update-event")]
        public async Task<IActionResult> PutEvent([FromBody] UpdateEventDTO updateEventDTO)
        {
            await _eventService.PutEvent(updateEventDTO);
            return NoContent();
        }

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<EventDTO>> GetEventById(Guid eventId)
        {
            var eventDetail = await _eventService.GetEventById(eventId);
            if (eventDetail == null)
            {
                return NotFound();
            }
            return Ok(eventDetail);
        }
    }
}
