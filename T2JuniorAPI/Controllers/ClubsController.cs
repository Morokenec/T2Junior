using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase 
    { 
        private readonly IClubService _clubService;
        private readonly ApplicationDbContext _context;

        public ClubsController(ApplicationDbContext context, IClubService clubService)
        {
            _clubService = clubService;
            _context = context;
        }

        // GET: api/Clubs
        [HttpGet("by_user/{userId}")]
        public async Task<ActionResult<List<AllClubsDTO>>> GetAllClubsByUserId(Guid userId)
        {
            return await _clubService.GetAllClubsByUserId(userId);
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }

        [HttpGet("{clubId}/profile")]
        public async Task<IActionResult> GetClubProfile(Guid clubId)
        {
            var clubProfile = await _clubService.GetClubProfileById(clubId);
            return Ok(clubProfile);
        }

        [HttpGet("{clubId}/info")]
        public async Task<IActionResult> GetClubInfoById(Guid clubId)
        {
            try
            {
                var clubInfo = await _clubService.GetClubInfoById(clubId);
                return Ok(clubInfo);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message); // Возвращаем 404, если клуб не найден
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // Обработка других ошибок
            }
        }

        // PUT: api/Clubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(Guid id, [FromBody] UpdateClubDTO updateClubDTO)
        {
            try
            {
                var result = await _clubService.UpdateClub(id, updateClubDTO);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Clubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateClub([FromBody] CreateClubDTO club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clubService.CreateClub(club);
            return Ok(result);
        }

        [HttpPost("{clubId}/addUser")]
        public async Task<IActionResult> AddUserToClub(Guid clubId, [FromBody] AddUserToClubDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _clubService.AddUserToClub(clubId, user);
            return Ok(result);
        }

        // DELETE: api/Clubs/{clubId}/deleteUser/{userId}
        [HttpDelete("{clubId}/deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserFromClub(Guid clubId, Guid userId)
        {
            var result = await _clubService.DeleteUserFromClub(clubId, userId);
            return Ok(result);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            try
            {
                var result = await _clubService.DeleteClub(id);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
