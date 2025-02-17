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
    /// Сервис для работы с медиафайлами
    /// </summary>
    public class MediafileService : IMediafileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediaTypeService _mediaTypeService;


        /// <summary>
        /// Конструктор для внедрения зависимостей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="mapper">Маппер для преобразования объектов</param>
        /// <param name="mediaTypeService">Сервис для работы с типами медиа</param>
        /// <param name="user">Менеджер пользователей</param>
        public MediafileService(ApplicationDbContext context, IMapper mapper, IMediaTypeService mediaTypeService, UserManager<ApplicationUser> user)
        {
            _context = context;
            _mapper = mapper;
            _mediaTypeService = mediaTypeService;
            _userManager = user;
        }

        /// <summary>
        /// Добавление медиафайла для пользователя
        /// </summary>
        /// <param name="uploadDTO">Данные загружаемого файла</param>
        /// <returns>Модель медиафайла</returns>
        public async Task<Mediafile> AddMediaByUserId(MediafileUploadDTO uploadDTO)
        {
            return await CreateMediafileAsync(uploadDTO);

        }

        /// <summary>
        /// Установка аватара для пользователя
        /// </summary>
        /// <param name="uploadDTO">Данные загружаемого файла</param>
        /// <returns>Сообщение об успешной установке аватара</returns>
        public async Task<string> SetAvatarByUserId(MediafileUploadDTO uploadDTO)
        {
            var mediafile = await CreateMediafileAsync(uploadDTO, true);
            await SetAvatarForUserAsync(uploadDTO.IdUser, mediafile.Id);
            return "Avatar set successfuiiy";
        }

        /// <summary>
        /// Создание медиафайла
        /// </summary>
        /// <param name="uploadDTO">Данные загружаемого файла</param>
        /// <param name="isImage">Флаг, указывающий, является ли файл изображением</param>
        /// <returns>Модель медиафайла</returns>
        public async Task<Mediafile> CreateMediafileAsync(MediafileUploadDTO uploadDTO, bool isImage = false)
        {
            try
            {
                var mediafile = _mapper.Map<Mediafile>(uploadDTO);
                mediafile.IdType = await GetMediaTypeId(uploadDTO.File.FileName);
                mediafile.Path = (await SaveFileAsync(uploadDTO.File)).Replace("wwwroot\\uploads\\", "uploads/").Replace("\\", "/");
                

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
        /// Сохраняет файл в папку uploads и возвращает путь к файлу
        /// </summary>
        /// <param name="file">Файл для сохранения</param>
        /// <returns>Путь к сохраненному файлу</returns>
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine("wwwroot", "uploads");
            
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var path = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        /// <summary>
        /// Возвращает Id типа медиафайла на основе расширения файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Id типа медиафайла</returns>
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
        /// Устанавливает аватар для пользователя
        /// </summary>
        /// <param name="idUser">Id пользователя</param>
        /// <param name="mediafileId">Id медиафайла</param>
        private async Task SetAvatarForUserAsync(Guid idUser, Guid mediafileId)
        {
            var userAvatar = _mapper.Map<UserAvatar>(new UserAvatarDTO { IdUser = idUser, IdMedia =  mediafileId });
            _context.UserAvatars.Add(userAvatar);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет аватар пользователя
        /// </summary>
        /// <param name="deleteDTO">Данные для удаления аватара</param>
        /// <returns>Сообщение об успешном удалении аватара</returns>
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
        /// Удаляет медиафайл пользователя
        /// </summary>
        /// <param name="mediafileDeleteDTO">Данные для удаления медиафайла</param>
        /// <returns>Сообщение об успешном удалении медиафайла</returns>
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
        /// Возвращает все медиафайлы пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Коллекция медиафайлов</returns>
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
