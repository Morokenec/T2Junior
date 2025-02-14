using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.DTOs.Medias;
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
            try
            {
                var achievements = await _achievementService.GetAchievementsAllByUserIdAsync(id);
                return Ok(achievements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("by-user-id/{id}")]
        public async Task<ActionResult<IEnumerable<AchievementDTO>>> GetAchievementsByUserId(Guid id)
        {
            try
            {
                var achievements = await _achievementService.GetAchievementsByUserIdAsync(id);
                return Ok(achievements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<AchievementDTO>> PostAchievement([FromForm] CreateAchievementDTO achievementDto, [FromForm] MediafileUploadDTO uploadDTO = null)
        {
            try
            {
                var achievement = await _achievementService.CreateAchievementAsync(achievementDto, uploadDTO);
                return Ok(achievement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutAchievement([FromForm] UpdateAchievementDTO achievementDto, [FromForm] MediafileUploadDTO uploadDTO = null)
        {
            try
            {
                var achievement = await _achievementService.UpdateAchievementAsync(achievementDto, uploadDTO);
                return Ok(achievement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("to-user")]
        public async Task<ActionResult> PostAchievementToUser(Guid userId, Guid achievementId)
        {
            try
            {
                await _achievementService.AssignAchievementToUserAsync(userId, achievementId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            try
            {
                await _achievementService.DeleteAchievementAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> ActivateAchievement(Guid id)
        {
            try
            {
                await _achievementService.ActivateAchievementAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> DeactivateAchievement(Guid id)
        {
            try
            {
                await _achievementService.DeactivateAchievementAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
