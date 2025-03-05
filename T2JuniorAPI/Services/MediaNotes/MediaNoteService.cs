using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.MediaNotes
{
    /// <summary>
    /// Сервис для управления медиафайлами в заметках.
    /// </summary>
    public class MediaNoteService : IMediaNoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileservice;

        /// <summary>
        /// Конструктор класса MediaNoteService.
        /// </summary>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="mediafileservice">Сервис для работы с медиафайлами.</param>
        public MediaNoteService(IMapper mapper, ApplicationDbContext context, IMediafileService mediafileservice)
        {
            _mapper = mapper;
            _context = context;
            _mediafileservice = mediafileservice;
        }

        /// <summary>
        /// Добавление медиафайла к заметке по её идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="uploadDTO">DTO с данными для загрузки медиафайла.</param>
        /// <returns>Созданная связь между заметкой и медиафайлом.</returns>
        public async Task<MediaNoteDTO> AddMediaToNoteAsync(Guid idNote, MediafileUploadDTO uploadDTO)
        {
            var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO);

            var mediaNote = new MediaNote { IdNote = idNote, IdMedia = mediafile.Id };

            await _context.MediaNotes.AddAsync(mediaNote);
            await _context.SaveChangesAsync();

            return _mapper.Map<MediaNoteDTO>(mediaNote);
        }

        /// <summary>
        /// Удаление медиафайла из заметки по её идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <param name="idMedia">Идентификатор медиафайла.</param>
        /// <returns>Результат удаления медиафайла.</returns>
        public async Task<bool> DeleteMediaFromNoteAsync(Guid idNote, Guid idMedia)
        {
            var mediaNote = await _context.MediaNotes
                .FirstOrDefaultAsync(mn  => mn.IdNote == idNote && mn.IdMedia == idMedia && !mn.IsDelete);

            if (mediaNote == null) return false;

            mediaNote.IsDelete = true;
            mediaNote.UpdateDate = DateTime.Now;

            var note = await _context.Notes.FindAsync(idNote);
            var wall = await _context.Walls.FirstOrDefaultAsync(w => w.Id == note.IdWall);
            Guid? ownerId = wall.IdUserOwner ?? wall.IdClubOwner;

            if (ownerId.HasValue)
                await _mediafileservice.DeleteMediaByUserId(new MediafileDeleteDTO { UserId = ownerId.Value, MediaId = idMedia });
            else
                throw new ApplicationException("Owner not found");

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
