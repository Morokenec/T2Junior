using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.DTOs.Medias;
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

        /// <summary>
        /// Получение календаря событий пользователя за указанный месяц и год
        /// </summary>
        /// <param name="userId">Пользователь</param>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Получение календаря событий клуба за указанный месяц и год
        /// </summary>
        /// <param name="clubId">Пользователь</param>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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


        /// <summary>
        /// Создание новых событий
        /// </summary>
        /// <param name="createEventDTO">Событие</param>
        /// <param name="mediafile">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("create-event")]
        public async Task<ActionResult<Guid>> CreateEvent([FromForm] CreateEventDTO createEventDTO, [FromForm] MediafileUploadDTO mediafile)
        {
            try
            {
                var eventId = await _eventService.CreateEvent(createEventDTO, mediafile);
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

        /// <summary>
        /// Удаление события по его ID
        /// </summary>
        /// <param name="eventId">Событие</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            await _eventService.DeleteEvent(eventId);
            return NoContent();
        }

        /// <summary>
        /// Обновление существующего события
        /// </summary>
        /// <param name="updateEventDTO">Событие</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut("update-event")]
        public async Task<IActionResult> PutEvent([FromBody] UpdateEventDTO updateEventDTO)
        {
            await _eventService.PutEvent(updateEventDTO);
            return NoContent();
        }

        /// <summary>
        /// Получение информации о событии по его ID
        /// </summary>
        /// <param name="eventId">Событие</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{eventId}")]
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
