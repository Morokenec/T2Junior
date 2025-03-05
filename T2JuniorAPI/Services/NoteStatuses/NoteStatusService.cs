using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.Entities;
using T2JuniorAPI.DTOs.Notes;

namespace T2JuniorAPI.Services.NoteStatuses
{
    /// <summary>
    /// Сервис для управления статусами заметок.
    /// </summary>
    public class NoteStatusService : INoteStatusService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса NoteStatusService.
        /// </summary>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="context">Контекст базы данных.</param>
        public NoteStatusService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        /// <summary>
        /// Получение или создание статуса заметки по его названию.
        /// </summary>
        /// <param name="name">Название статуса заметки.</param>
        /// <returns>Статус заметки.</returns>
        public async Task<NoteStatusDTO> GetOrCreateNoteStatusAsync(string name)
        {
            var noteStatus = await _context.NoteStatuses.FirstOrDefaultAsync(ns => ns.Name == name);

            if (noteStatus == null)
            {
                noteStatus = new NoteStatus { Name = name };
                await _context.NoteStatuses.AddAsync(noteStatus);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<NoteStatusDTO>(noteStatus);
        }

        /// <summary>
        /// Обновление существующего статус заметки.
        /// </summary>
        /// <param name="noteStatusDTO">DTO с обновленными данными статуса заметки.</param>
        /// <returns>Обновленный статус заметки.</returns>
        public async Task<NoteStatusDTO> UpdateNoteStatusAsync(NoteStatusDTO noteStatusDTO)
        {
            var noteStatus = await _context.NoteStatuses.FindAsync(noteStatusDTO.Id);
            if (noteStatus == null)
                throw new ApplicationException("Note status not found");

            noteStatus.UpdateDate = DateTime.Now;
            _mapper.Map(noteStatusDTO, noteStatus);
            await _context.SaveChangesAsync();
            return _mapper.Map<NoteStatusDTO>(noteStatus);
        }

        /// <summary>
        /// Удаление статуса заметки по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса заметки.</param>
        /// <returns>Результат удаления статуса заметки.</returns>
        public async Task<bool> DeleteNoteStatusAsync(Guid id)
        {
            var noteStatus = await _context.NoteStatuses.FindAsync(id);
            if (noteStatus == null)
                throw new ApplicationException("Note status not found");

            noteStatus.IsDelete = true;
            noteStatus.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получение всех статусов заметок.
        /// </summary>
        /// <returns>Список всех статусов заметок.</returns>
        public async Task<IEnumerable<NoteStatusDTO>> GetAllNoteStatusesAsync()
        {
            var noteStatuses = await _context.NoteStatuses.ToListAsync();
            return _mapper.Map<IEnumerable<NoteStatusDTO>>(noteStatuses);
        }
    }
}
