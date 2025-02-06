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
using T2JuniorAPI.Services;

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

        [HttpGet]
        public async Task<ActionResult<List<EventDirectionDTO>>> GetEventDirection()
        {
            var directions = await _eventService.GetEventDirection();
            return Ok(directions);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEventDirection([FromBody] EventDirectionDTO createEventDirectionDto)
        {
            var directionId = await _eventService.CreateEventDirection(createEventDirectionDto);
            return Ok(directionId);
        }

        [HttpDelete("{directionId}")]
        public async Task<IActionResult> DeleteEventDirection(Guid directionId)
        {
            await _eventService.DeleteEventDirection(directionId);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutEventDirection([FromBody] EventDirectionDTO updateEventDirectionDto)
        {
            await _eventService.PutEventDirection(updateEventDirectionDto);
            return NoContent();
        }

        private bool EventDirectionExists(Guid id)
        {
            return _context.EventDirections.Any(e => e.Id == id);
        }
    }
}
