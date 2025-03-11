using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.MediaInitiatives
{
    /// <summary>
    /// Сервис для управления медиафайлами в инициативах.
    /// </summary>
    public class MediaInitiativeService : IMediaInitiativeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileService;

        /// <summary>
        /// Конструктор класса MediaInitiativeService.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="mediafileService">Сервис для работы с медиафайлами.</param>
        public MediaInitiativeService(ApplicationDbContext context, IMapper mapper, IMediafileService mediafileService)
        {
            _context = context;
            _mapper = mapper;
            _mediafileService = mediafileService;
        }

        /// <summary>
        /// Добавление медиафайла к инициативе по её идентификатору.
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы.</param>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Созданная связь между инициативой и медиафайлом.</returns>
        public async Task<MediaInitiative> AddMediaToInitiativeAsync(Guid initiativeId, MediafileUploadDTO uploadDTO)
        {
            var mediafile = await _mediafileService.CreateMediafileAsync(uploadDTO);

            var mediaInitiative = new MediaInitiative
            {
                IdInitiative = initiativeId,
                IdMedia = mediafile.Id
            };

            _context.MediaInitiatives.Add(mediaInitiative);
            await _context.SaveChangesAsync();

            return mediaInitiative;
        }

        /// <summary>
        /// Удаление медиафайла из инициативы по её идентификатору.
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы.</param>
        /// <param name="mediaId">Идентификатор медиафайла.</param>
        /// <returns>Результат удаления медиафайла.</returns>
        public async Task<bool> DeleteMediaFromInitiativeAsync(Guid initiativeId, Guid mediaId)
        {
            var mediaInitiative = await _context.MediaInitiatives
                .FirstOrDefaultAsync(mi => mi.IdInitiative == initiativeId && mi.IdMedia == mediaId && !mi.IsDelete);

            if (mediaInitiative == null) return false;

            mediaInitiative.IsDelete = true;
            mediaInitiative.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получение всех медиафайлов для инициативы по её идентификатору.
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы.</param>
        /// <returns>Список медиафайлов, связанных с инициативой.</returns>
        public async Task<IEnumerable<MediafileDTO>> GetAllMediaForInitiativeAsync(Guid initiativeId)
        {
            var mediaInitiatives = await _context.MediaInitiatives
                .Include(mi => mi.Mediafile)
                    .ThenInclude(mf => mf.IdMediaTypesNavigation)
                .Where(mi => mi.IdInitiative == initiativeId && !mi.IsDelete && !mi.Mediafile.IsDelete)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MediafileDTO>>(mediaInitiatives.Select(mi => mi.Mediafile));
        }
    }
}

