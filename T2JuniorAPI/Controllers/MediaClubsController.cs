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
