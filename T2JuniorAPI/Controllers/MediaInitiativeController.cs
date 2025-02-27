using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Services.MediaInitiatives;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaInitiativeController : ControllerBase
    {
        private readonly IMediaInitiativeService _mediaInitiativeService;

        public MediaInitiativeController(IMediaInitiativeService mediaInitiativeService)
        {
            _mediaInitiativeService = mediaInitiativeService;
        }

        /// <summary>
        /// Добавление медиафайла к инициативе
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы</param>
        /// <param name="uploadDTO">Медиафайл</param>
        /// <returns></returns>
        [HttpPost("add/{initiativeId}")]
        public async Task<IActionResult> AddMediaToInitiative(Guid initiativeId, [FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var mediaInitiative = await _mediaInitiativeService.AddMediaToInitiativeAsync(initiativeId, uploadDTO);
                return Ok(mediaInitiative);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаление медиафайла из инициативы
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы</param>
        /// <param name="mediaId">Идентификатор медиафайла</param>
        /// <returns></returns>
        [HttpDelete("delete/{initiativeId}/{mediaId}")]
        public async Task<IActionResult> DeleteMediaFromInitiative(Guid initiativeId, Guid mediaId)
        {
            var result = await _mediaInitiativeService.DeleteMediaFromInitiativeAsync(initiativeId, mediaId);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Получение всех медиафайлов для инициативы
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы</param>
        /// <returns></returns>
        [HttpGet("{initiativeId}")]
        public async Task<IActionResult> GetAllMediaForInitiative(Guid initiativeId)
        {
            var mediafiles = await _mediaInitiativeService.GetAllMediaForInitiativeAsync(initiativeId);
            if (mediafiles == null || !mediafiles.Any()) return NotFound();
            return Ok(mediafiles);
        }
    }
}
