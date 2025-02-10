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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadMediaFile([FromForm] MediafileUploadDTO uploadDTO)
        {
            try
            {
                var mediafile = await _mediafileService.UploadMediafileAsync(uploadDTO);

                var mediafileDTO = _mapper.Map<MediafileDTO>(mediafile);

                return Ok(mediafileDTO);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading file: {ex.Message}");
                return StatusCode(500, "Internal Custone Error");
            }

            
        }

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
    }
}
