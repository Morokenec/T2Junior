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

        [HttpGet("all-by-user-id/{id}")]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievementsAllByUserId(Guid id)
        {
            return ReturnOK();
        }
        
        [HttpGet("by-user-id/{id}")]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievementsByUserId(Guid id)
        {
            return ReturnOK();
        }

        // PUT: api/Achievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievement(Guid id, Achievement achievement)
        {
            return ReturnOK();
        }

        // POST: api/Achievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Achievement>> PostAchievement(Achievement achievement)
        {
            return ReturnOK();
        }
        
        [HttpPost("to-user")]
        public async Task<ActionResult<Achievement>> PostAchievementToUser(Achievement achievement)
        {
            return ReturnOK();
        }

        // DELETE: api/Achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            return ReturnOK();
        }

        private bool AchievementExists(Guid id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }

        private OkObjectResult ReturnOK()
        {
            return Ok("API решил уйти в отпуск, подожди немного или принеси ему кофе =)");
        }
    }
}
