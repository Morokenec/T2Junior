using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Events;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDirectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventService _eventService;

        public EventDirectionsController(ApplicationDbContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        /// <summary>
        /// Получение списка направлений событий.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet]
        public async Task<ActionResult<List<EventDirectionDTO>>> GetEventDirection()
        {
            var directions = await _eventService.GetEventDirection();
            return Ok(directions);
        }

        /// <summary>
        /// Создание нового направления события.
        /// </summary>
        /// <param name="createEventDirectionDto">Направление событий</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEventDirection([FromBody] EventDirectionDTO createEventDirectionDto)
        {
            var directionId = await _eventService.CreateEventDirection(createEventDirectionDto);
            return Ok(directionId);
        }

        /// <summary>
        /// Удаление направления события по ID.
        /// </summary>
        /// <param name="directionId">Направление событий</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{directionId}")]
        public async Task<IActionResult> DeleteEventDirection(Guid directionId)
        {
            await _eventService.DeleteEventDirection(directionId);
            return NoContent();
        }

        /// <summary>
        /// Обновление существующего направления события.
        /// </summary>
        /// <param name="updateEventDirectionDto">Направление событий</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut]
        public async Task<IActionResult> PutEventDirection([FromBody] EventDirectionDTO updateEventDirectionDto)
        {
            await _eventService.PutEventDirection(updateEventDirectionDto);
            return NoContent();
        }
    }
}
