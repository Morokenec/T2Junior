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

        /// <summary>
        /// Получение всех достижений, с выделением пользовательских по его ID.
        /// </summary>
        /// <param name="id">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Получение достижения пользователя по ID.
        /// </summary>
        /// <param name="id">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Создание нового достижения.
        /// </summary>
        /// <param name="achievementDto">Достижение</param>
        /// <param name="uploadDTO">Медиафайл</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Обновление существующего достижение.
        /// </summary>
        /// <param name="achievementDto">Достижения</param>
        /// <param name="uploadDTO">Медиафайл</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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


        /// <summary>
        /// Выдача достижения пользователю.
        /// </summary>
        /// <param name="userId)">Пользователь</param>
        /// <param name="achievementId)">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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


        /// <summary>
        /// Удаление достижения.
        /// </summary>
        /// <param name="id">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Активация достижения.
        /// </summary>
        /// <param name="id)">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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

        /// <summary>
        /// Деактивация достижения.
        /// </summary>
        /// <param name="id)">Достижения</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
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
