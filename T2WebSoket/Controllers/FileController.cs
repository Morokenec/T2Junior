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

        /// <summary>
        /// Конструктор класса FileController.
        /// </summary>
        /// <param name="fileService">Сервис для работы с файлами.</param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Загрузка файла на сервер.
        /// </summary>
        /// <param name="fileUploadDTO">DTO с данными для загрузки файла.</param>
        /// <returns>Результат загрузки файла.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            var file = await _fileService.UploadFileAsync(fileUploadDTO);
            return Ok(file);
        }

        /// <summary>
        /// Полуение файла по его идентификатору.
        /// </summary>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Файл, если найден.</returns>
        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await _fileService.GetFileByIdAsync(fileId);
            return Ok(file);
        }

        /// <summary>
        /// Удаление файла по его идентификатору.
        /// </summary>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Результат удаления файла.</returns>
        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            var result = await _fileService.DeleteFileAsync(fileId);
            return Ok(result);
        }
    }
}
