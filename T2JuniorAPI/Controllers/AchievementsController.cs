using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Achievements;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("all-by-user-id/{id}")]
        public async Task<ActionResult<IEnumerable<AchievementDTO>>> GetAchievementsAllByUserId(Guid id)
        {
            var achievements = await _achievementService.GetAchievementsAllByUserIdAsync(id);
            return Ok(achievements);
        }

        [HttpGet("by-user-id/{id}")]
        public async Task<ActionResult<IEnumerable<AchievementDTO>>> GetAchievementsByUserId(Guid id)
        {
            var achievements = await _achievementService.GetAchievementsByUserIdAsync(id);
            return Ok(achievements);
        }

        [HttpPost]
        public async Task<ActionResult<AchievementDTO>> PostAchievement([FromForm] CreateAchievementDTO achievementDto, [FromForm] MediafileUploadDTO uploadDTO = null)
        {
            var achievement = await _achievementService.CreateAchievementAsync(achievementDto, uploadDTO);
            return Ok(achievement);
        }

        [HttpPut]
        public async Task<IActionResult> PutAchievement([FromForm] UpdateAchievementDTO achievementDto, [FromForm] MediafileUploadDTO uploadDTO = null)
        {
            var achievement = await _achievementService.UpdateAchievementAsync(achievementDto, uploadDTO);
            return Ok(achievement);
        }

        [HttpPost("to-user")]
        public async Task<ActionResult> PostAchievementToUser(Guid userId, Guid achievementId)
        {
            await _achievementService.AssignAchievementToUserAsync(userId, achievementId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            await _achievementService.DeleteAchievementAsync(id);
            return Ok();
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> ActivateAchievement(Guid id)
        {
            await _achievementService.ActivateAchievementAsync(id);
            return Ok();
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> DeactivateAchievement(Guid id)
        {
            await _achievementService.DeactivateAchievementAsync(id);
            return Ok();
        }
    }
}
