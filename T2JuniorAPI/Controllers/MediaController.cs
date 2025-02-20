using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediafileService _mediafileService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public MediaController(IMediafileService mediafileService, IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _mediafileService = mediafileService;
            _mapper = mapper;
            _context = applicationDbContext;
        }

        /// <summary>
        /// Добавление медиафайла пользователем
        /// </summary>
        /// <param name="uploadDTO">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("add-by-user-id")]
        public async Task<IActionResult> UploadMediaFile([FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var mediafile = await _mediafileService.AddMediaByUserId(uploadDTO);

                var mediafileDTO = _mapper.Map<MediafileDTO>(mediafile);

                return Ok(mediafileDTO);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading file: {ex.Message}");
                return StatusCode(500, "Internal Custone Error");
            }
        }

        /// <summary>
        /// Скачивание медиафайлов пользователем
        /// </summary>
        /// <param name="id">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadMediafile(Guid id)
        {
            var mediafile = await _context.Mediafiles.FirstOrDefaultAsync(m => m.Id == id);
            if (mediafile == null) 
                return NotFound("Mediafile not found");

            var filepath = mediafile.Path;
            if (!System.IO.File.Exists(filepath))
                return NotFound("File not found on the server.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filepath));
        }

        /// <summary>
        /// Удаление медиафайлов пользователем
        /// </summary>
        /// <param name="mediafileDeleteDTO">Медиафайлы</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete-by-user-id")]
        public async Task<IActionResult> DeleteMediaByUserId([FromBody] MediafileDeleteDTO mediafileDeleteDTO)
        {
            try
            {
                var result = await _mediafileService.DeleteMediaByUserId(mediafileDeleteDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting media files: {ex.Message}");
                return StatusCode(500, "Internal Custom Error");
            }
        }

        /// <summary>
        /// Получения всех медиафайлов, связанных с пользователем
        /// </summary>
        /// <param name="userId">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("get-all-by-user-id")]
        public async Task<IActionResult> GetAllMediaByUserId([FromQuery] Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                    return BadRequest("Invalid userId");

                var mediafiles = await _mediafileService.GetAllMediaByUserId(userId);
                if (mediafiles == null || !mediafiles.Any())
                    return NotFound("No mediafiles found");

                return Ok(mediafiles);
            }
            catch (Exception ex)
            {

                Console.Error.WriteLine($"Error retrieving media files: {ex.Message}");
                return StatusCode(500, "Internal Custom Error");
            }
        }

        /// <summary>
        /// Установка аватара пользователем
        /// </summary>
        /// <param name="uploadDTO">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("set-avatar-for-user")]
        public async Task<IActionResult> SetAvaterByUserId([FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var result = await _mediafileService.SetAvatarByUserId(uploadDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error setting avatar: {ex.Message}");
                return StatusCode(500, "Internal Custom Error");
            }
        }

        /// <summary>
        /// Удаление аватара пользователем
        /// </summary>
        /// <param name="deleteDTO">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("delete-avatar-by-user-id")]
        public async Task<IActionResult> DeleteAvatarByUserId([FromBody] MediafileDeleteDTO deleteDTO)
        {
            try
            {
                var result = await _mediafileService.DeleteAvatarByUser(deleteDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting avatar: {ex.Message}");
                return StatusCode(500, "Internal Custom Error");
            }
        }
    }
}
