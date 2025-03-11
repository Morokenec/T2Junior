using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2WebSoket.DTOs;
using T2WebSoket.Services;

namespace T2WebSoket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            var file = await _fileService.UploadFileAsync(fileUploadDTO);
            return Ok(file);
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await _fileService.GetFileByIdAsync(fileId);
            return Ok(file);
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            var result = await _fileService.DeleteFileAsync(fileId);
            return Ok(result);
        }
    }
}
