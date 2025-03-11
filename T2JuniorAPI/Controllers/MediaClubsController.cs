using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Services.MediaClubs;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaClubsController : ControllerBase
    {
        private readonly IMediaClubService _mediaClubService;

        public MediaClubsController(IMediaClubService mediaClubService)
        {
            _mediaClubService = mediaClubService;
        }


        /// <summary>
        /// Добавление медиафайла в клуб.
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="uploadDTO">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add/{clubId}")]
        public async Task<IActionResult> AddMediaByClubId(Guid clubId, [FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var result = await _mediaClubService.AddMediaByClubId(uploadDTO, clubId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Удаление медиафайла из клуба.
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="mediaId">Медиафайлы</param>
        /// <param name="userId">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete/{clubId}/{mediaId}")]
        public async Task<IActionResult> DeleteMediaByClubId(Guid clubId, Guid mediaId, [FromQuery] Guid userId)
        {
            try
            {
                var result = await _mediaClubService.DeleteMediaByClubId(clubId, mediaId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Получение всех медиафайлов, связанных с клубом.
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{clubId}")]
        public async Task<IActionResult> GetAllMediaByClubId(Guid clubId)
        {
            try
            {
                var result = await _mediaClubService.GetAllMediaByClubId(clubId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
                throw;
            }
        }

        /// <summary>
        /// Установка аватара для клуба.
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="uploadDTO">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("set-avatar/{clubId}")]
        public async Task<IActionResult> SetAvatarForClub(Guid clubId, [FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var result = await _mediaClubService.SetAvatarForClub(uploadDTO, clubId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Удаление аватара из клуба.
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="mediaId">Медиафайлы</param>
        /// <param name="userId">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete-avatar")]
        public async Task<IActionResult> DeleteAvatarFromClub([FromQuery] Guid clubId, [FromQuery] Guid mediaId, [FromQuery] Guid userId)
        {
            try
            {
                var result = await _mediaClubService.DeleteAvatarFromClub(clubId, mediaId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
