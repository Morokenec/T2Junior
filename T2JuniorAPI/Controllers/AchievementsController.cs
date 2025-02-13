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
    public class AchievementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AchievementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Achievements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievements()
        {
            return await _context.Achievements.ToListAsync();
        }

        // GET: api/Achievements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchievement(Guid id)
        {
            var achievement = await _context.Achievements.FindAsync(id);

            if (achievement == null)
            {
                return NotFound();
            }

            return achievement;
        }

        // PUT: api/Achievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievement(Guid id, Achievement achievement)
        {
            if (id != achievement.Id)
            {
                return BadRequest();
            }

            _context.Entry(achievement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementExists(id))
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

        // POST: api/Achievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Achievement>> PostAchievement(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAchievement", new { id = achievement.Id }, achievement);
        }

        // DELETE: api/Achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }

            _context.Achievements.Remove(achievement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AchievementExists(Guid id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }
    }
}
