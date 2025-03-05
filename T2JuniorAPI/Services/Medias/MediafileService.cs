using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.DTOs.MediaTypes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaTypes;

namespace T2JuniorAPI.Services.Medias
{
    /// <summary>
    /// Сервис для управления медиафайлами.
    /// </summary>
    public class MediafileService : IMediafileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediaTypeService _mediaTypeService;

        /// <summary>
        /// Конструктор класса MediafileService.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="mediaTypeService">Сервис для работы с типами медиафайлов.</param>
        /// <param name="user">Менеджер пользователей.</param>
        public MediafileService(ApplicationDbContext context, IMapper mapper, IMediaTypeService mediaTypeService, UserManager<ApplicationUser> user)
        {
            _context = context;
            _mapper = mapper;
            _mediaTypeService = mediaTypeService;
            _userManager = user;
        }

        /// <summary>
        /// Добавление медиафайа для пользователя.
        /// </summary>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Созданный медиафайл.</returns>
        public async Task<Mediafile> AddMediaByUserId(MediafileUploadDTO uploadDTO)
        {
            return await CreateMediafileAsync(uploadDTO);

        }

        /// <summary>
        /// Установка аватара для пользователя.
        /// </summary>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Сообщение об успешной установке аватара.</returns>
        public async Task<string> SetAvatarByUserId(MediafileUploadDTO uploadDTO)
        {
            var mediafile = await CreateMediafileAsync(uploadDTO, true);
            await SetAvatarForUserAsync(uploadDTO.IdUser, mediafile.Id);
            return "Avatar set successfuiiy";
        }

        /// <summary>
        /// Создание медиафайла.
        /// </summary>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <param name="isImage">Флаг, указывающий, является ли файл изображением.</param>
        /// <returns>Созданный медиафайл.</returns>
        public async Task<Mediafile> CreateMediafileAsync(MediafileUploadDTO uploadDTO, bool isImage = false)
        {
            try
            {
                var mediaId = Guid.NewGuid();
                var fileExtension = uploadDTO.File.FileName.ToLower().Replace(" ", "");
                var newFileName = $"{mediaId}_{fileExtension}";

                var mediafile = _mapper.Map<Mediafile>(uploadDTO);
                mediafile.Id = mediaId;
                mediafile.IdType = await GetMediaTypeId(uploadDTO.File.FileName);
                mediafile.Path = (await SaveFileAsync(uploadDTO.File, newFileName)).Replace("wwwroot\\uploads\\", "uploads/").Replace("\\", "/");
                

                if (isImage && mediafile.IdType != (await _mediaTypeService.GetGuidOrCreateMediaType(new MediaTypeDTO { Name = "Image" })).Id)
                    throw new InvalidOperationException("mediafile must be an image file");

                _context.Mediafiles.Add(mediafile);
                await _context.SaveChangesAsync();

                return mediafile;
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

        /// <summary>
        /// Определение типа медиафайла по расширению.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Идентификатор типа медиафайла.</returns>
        private async Task<Guid> GetMediaTypeId(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return (await _mediaTypeService.GetGuidOrCreateMediaType(new MediaTypeDTO { Name = "Image" })).Id;
                case ".mp4":
                case ".avi":
                case ".mkv":
                    return (await _mediaTypeService.GetGuidOrCreateMediaType(new MediaTypeDTO { Name = "Video" })).Id;

                default:
                    throw new InvalidOperationException("Unsupported file type");
            }
        }

        /// <summary>
        /// Установка аватара для пользователя.
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя.</param>
        /// <param name="mediafileId">Идентификатор медиафайла.</param>
        private async Task SetAvatarForUserAsync(Guid idUser, Guid mediafileId)
        {
            var userAvatar = _mapper.Map<UserAvatar>(new UserAvatarDTO { IdUser = idUser, IdMedia =  mediafileId });
            _context.UserAvatars.Add(userAvatar);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление аватара пользователя.
        /// </summary>
        /// <param name="deleteDTO">DTO с данными для удаления медиафайла.</param>
        /// <returns>Сообщение об успешном удалении аватара.</returns>
        public async Task<string> DeleteAvatarByUser(MediafileDeleteDTO deleteDTO)
        {
            var userAvatar = await _context.UserAvatars
                .Where(ua => ua.IdUser == deleteDTO.UserId && ua.IdMedia == deleteDTO.MediaId && !ua.IsDelete)
                .FirstOrDefaultAsync();

            if (userAvatar == null)
                return "Avatar not found";

            userAvatar.IsDelete = true;
            userAvatar.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            await DeleteMediaByUserId(deleteDTO);

            return "Avatar Deleted Successfully";
        }

        /// <summary>
        /// Удаление медиафайла по идентификатору пользователя.
        /// </summary>
        /// <param name="mediafileDeleteDTO">DTO с данными для удаления медиафайла.</param>
        /// <returns>Сообщение об успешном удалении медиафайла.</returns>
        public async Task<string> DeleteMediaByUserId(MediafileDeleteDTO mediafileDeleteDTO)
        {
            var mediafile = await _context.Mediafiles
                    .Where(m => m.IdUser == mediafileDeleteDTO.UserId && m.Id == mediafileDeleteDTO.MediaId && !m.IsDelete)
                    .FirstOrDefaultAsync(); 

            if (mediafile == null)
                return "Mediafile not found";

            mediafile.IsDelete = true;
            mediafile.UpdateDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting media files: {ex.Message}");
            }

            return "Success";
        }

        /// <summary>
        /// Получение всех медиафайлов пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список медиафайлов пользователя.</returns>
        public async Task<IEnumerable<MediafileDTO>> GetAllMediaByUserId(Guid userId)
        {
            try
            {
                var mediafiles = await _context.Mediafiles
                    .Include(m => m.IdMediaTypesNavigation)
                    .Where(m => m.IdUser == userId && !m.IsDelete)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<MediafileDTO>>(mediafiles);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving media files: {ex.Message}");
            }
        }

        
    }
}
