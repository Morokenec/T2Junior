using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
using T2WebSoket.Repositories;

namespace T2WebSoket.Services
{
    /// <summary>
    /// Сервис для управления файлами.
    /// </summary>
    public class FileService : IFileService
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса FileService.
        /// </summary>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="chatDbContext">Контекст базы данных для работы с файлами.</param>
        public FileService(IMapper mapper, ChatDbContext chatDbContext)
        {
            _mapper = mapper;
            _chatDbContext = chatDbContext;
        }

        /// <summary>
        /// Удаление файла по его идентификатору.
        /// </summary>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Результат удаления файла.</returns>
        public async Task<bool> DeleteFileAsync(Guid fileId)
        {
            var file = await _chatDbContext.Files.FindAsync(fileId);
            if (file == null)
            {
                throw new Exception("File not found");
            }

            file.IsDelete = true;
            file.UpdateDate = DateTime.Now;
            await _chatDbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получение файла по его идентификатору.
        /// </summary>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Файл, если найден.</returns>
        public async Task<FileDTO> GetFileByIdAsync(Guid fileId)
        {
            var file = await _chatDbContext.Files.FindAsync(fileId);
            return file == null ? throw new ApplicationException($"Could not find file {fileId}") : _mapper.Map<FileDTO>(file);
        }

        /// <summary>
        /// Загрузка файла на сервер.
        /// </summary>
        /// <param name="fileUpload">DTO с данными для загрузки файла.</param>
        /// <returns>Загруженный файл.</returns>
        public async Task<ChatFile> UploadFileAsync(FileUploadDTO fileUpload)
        {
            try
            {
                if (!Guid.TryParse(fileUpload.IdUser.ToString(), out Guid userId))
                {
                    throw new ArgumentException("Invalid userId format.");
                }

                var fileId = Guid.NewGuid();
                var fileExtension = fileUpload.File.FileName.ToLower().Replace(" ", "");
                var newFileName = $"{fileId}_{fileExtension}";
                var userIdGuid = Guid.Parse(fileUpload.IdUser);

                var file = _mapper.Map<ChatFile>(fileUpload);
                file.Id = fileId;
                file.FilePath = (await SaveFileAsync(fileUpload.File, newFileName)).Replace("wwwroot\\uploads\\", "uploads/").Replace("\\", "/");
                file.FileName = fileExtension;
                file.UserId = userIdGuid;

                _chatDbContext.Files.Add(file);
                await _chatDbContext.SaveChangesAsync();

                return file;
            }
            catch (DbUpdateException ex)
            {
                // Логирование ошибки
                Console.Error.WriteLine($"Database update error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Сохранение файла на сервере.
        /// </summary>
        /// <param name="file">Загружаемый файл.</param>
        /// <param name="newFileName">Новое имя файла.</param>
        /// <returns>Путь к сохраненному файлу.</returns>
        private async Task<string> SaveFileAsync(IFormFile file, string newFileName)
        {
            var uploadsFolder = Path.Combine("wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var path = Path.Combine(uploadsFolder, newFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
