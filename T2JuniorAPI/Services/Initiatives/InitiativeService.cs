using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Initiatives
{
    /// <summary>
    /// Сервис для управления инициативами.
    /// </summary>
    public class InitiativeService : IInitiativeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса InitiativeService.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        public InitiativeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание новой инициативы.
        /// </summary>
        /// <param name="initiativeDto">DTO с данными для создания инициативы.</param>
        /// <returns>Созданная инициатива.</returns>
        public async Task<InitiativeOutputDTO> CreateInitiativeAsync(InitiativeInputDTO initiativeDto)
        {
            // Найдите статус "Предложено"
            var proposedStatus = await _context.InitiativeStatuses
                .FirstOrDefaultAsync(s => s.Name == "Предложено");

            if (proposedStatus == null)
            {
                throw new InvalidOperationException("Статус 'Предложено' не найден.");
            }

            var initiative = _mapper.Map<Initiative>(initiativeDto);
            initiative.IdStatus = proposedStatus.Id; // Установите статус "Предложено"

            _context.Initiatives.Add(initiative);
            await _context.SaveChangesAsync();
            return _mapper.Map<InitiativeOutputDTO>(initiative);
        }

        /// <summary>
        /// Обновление существующей инициативы.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="initiativeDto">DTO с обновленными данными инициативы.</param>
        /// <returns>Обновленная инициатива.</returns>
        public async Task<InitiativeOutputDTO> UpdateInitiativeAsync(Guid id, InitiativeInputDTO initiativeDto)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return null;

            _mapper.Map(initiativeDto, initiative);
            await _context.SaveChangesAsync();
            return _mapper.Map<InitiativeOutputDTO>(initiative); 
        }

        /// <summary>
        /// Удаление инициативы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <returns>Результат удаления инициативы.</returns>
        public async Task<bool> DeleteInitiativeAsync(Guid id)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return false;

            initiative.IsDelete = true;
            initiative.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получение всех инициатив с деталями.
        /// </summary>
        /// <returns>Список всех инициатив с деталями.</returns>
        public async Task<IEnumerable<InitiativeOutputDTO>> GetAllInitiativesWithDetailsAsync()
        {
            var initiatives = await _context.Initiatives
                .Include(i => i.Status)
                .Include(i => i.Votes.Where(v => !v.IsDelete))
                .Include(i => i.InitiativeComments.Where(c => !c.IsDelete))
                    .ThenInclude(ic => ic.User)
                        .ThenInclude(u => u.UserAvatars)
                            .ThenInclude(ua => ua.Media)
                .Include(i => i.UserInitiatives.Where(ui => !ui.IsDelete))
                    .ThenInclude(ui => ui.User)
                .Include(i => i.MediaInitiatives.Where(mi => !mi.IsDelete))
                    .ThenInclude(mi => mi.Mediafile)
                        .ThenInclude(mf => mf.IdMediaTypesNavigation)
                .Where(i => !i.IsDelete)
                .OrderByDescending(i => i.Votes.Count + i.InitiativeComments.Count)
                .ToListAsync();

            var initiativeDtos = _mapper.Map<IEnumerable<InitiativeOutputDTO>>(initiatives);

            foreach (var initiativeDto in initiativeDtos)
            {
                var initiative = initiatives.First(i => i.Id == initiativeDto.Id);
                initiativeDto.StatusName = initiative.Status.Name;
                initiativeDto.VotesCount = initiative.Votes.Count(v => !v.IsDelete);
                initiativeDto.Comments = _mapper.Map<List<InitiativeCommentDTO>>(initiative.InitiativeComments.Where(c => !c.IsDelete));
                initiativeDto.Team = _mapper.Map<List<InitiativeUserDTO>>(initiative.UserInitiatives.Where(ui => !ui.IsDelete).Select(ui => ui.User));
                initiativeDto.Mediafiles = _mapper.Map<List<MediafileDTO>>(initiative.MediaInitiatives.Where(mi => !mi.IsDelete).Select(mi => mi.Mediafile));
            }

            return initiativeDtos;
        }

        /// <summary>
        /// Получение инициативы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <returns>Инициатива, если найдена; иначе, null.</returns>
        public async Task<InitiativeOutputDTO> GetInitiativeByIdAsync(Guid id)
        {
            var initiative = await _context.Initiatives
                .Include(i => i.Status)
                .Include(i => i.Votes.Where(v => !v.IsDelete))
                .Include(i => i.InitiativeComments.Where(c => !c.IsDelete))
                .ThenInclude(ic => ic.User)
                        .ThenInclude(u => u.UserAvatars)
                            .ThenInclude(ua => ua.Media)
                .Include(i => i.UserInitiatives.Where(ui => !ui.IsDelete))
                    .ThenInclude(ui => ui.User)
                .Include(i => i.MediaInitiatives.Where(mi => !mi.IsDelete))
                    .ThenInclude(mi => mi.Mediafile)
                        .ThenInclude(mf => mf.IdMediaTypesNavigation)
                .Where(i => !i.IsDelete)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDelete);

            if (initiative == null) return null;

            var initiativeDto = _mapper.Map<InitiativeOutputDTO>(initiative);
            initiativeDto.StatusName = initiative.Status.Name;
            initiativeDto.Team = _mapper.Map<List<InitiativeUserDTO>>(initiative.UserInitiatives.Where(ui => !ui.IsDelete).Select(ui => ui.User));
            initiativeDto.Mediafiles = _mapper.Map<List<MediafileDTO>>(initiative.MediaInitiatives.Where(mi => !mi.IsDelete).Select(mi => mi.Mediafile));

            return initiativeDto;
        }

        /// <summary>
        /// Голосование за инициативу.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя, голосующего за инициативу.</param>
        /// <returns>Результат голосования.</returns>
        public async Task<bool> VoteForInitiativeAsync(Guid id, Guid userId)
        {
            var vote = await _context.Votes
                .FirstOrDefaultAsync(v => v.IdInitiative == id && v.IdUser == userId);

            if (vote == null)
            {
                vote = new Vote { IdInitiative = id, IdUser = userId };
                _context.Votes.Add(vote);
            }
            else
            {
                vote.IsDelete = !vote.IsDelete;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Добавление комментария к инициативе.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="commentDto">DTO с данными для создания комментария.</param>
        /// <returns>Результат добавления комментария.</returns>
        public async Task<bool> CommentOnInitiativeAsync(Guid id, CreateInitiativeComment commentDto)
        {
            var comment = _mapper.Map<InitiativeComment>(commentDto);
            comment.IdInitiative = id;

            _context.InitiativeComments.Add(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Изменение статуса инициативы.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="statusId">Идентификатор нового статуса.</param>
        /// <returns>Результат изменения статуса.</returns>
        public async Task<bool> ChangeInitiativeStatusAsync(Guid id, Guid statusId)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return false;

            initiative.IdStatus = statusId;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Добавление пользователя к инициативе.
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Результат добавления пользователя.</returns>
        public async Task<bool> AddUserToInitiativeAsync(Guid initiativeId, Guid userId)
        {
            var userInitiative = new UserInitiative
            {
                IdInitiative = initiativeId,
                IdUser = userId
            };

            _context.UserInitiatives.Add(userInitiative);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удаление пользователя из инициативы.
        /// </summary>
        /// <param name="initiativeId">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Результат удаления пользователя.</returns>
        public async Task<bool> RemoveUserFromInitiativeAsync(Guid initiativeId, Guid userId)
        {
            var userInitiative = await _context.UserInitiatives
                .FirstOrDefaultAsync(ui => ui.IdInitiative == initiativeId && ui.IdUser == userId && !ui.IsDelete);

            if (userInitiative == null) return false;

            userInitiative.IsDelete = true;
            userInitiative.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получение всех статусов инициатив.
        /// </summary>
        /// <returns>Список всех статусов инициатив.</returns>
        public async Task<IEnumerable<InitiativeStatusDTO>> GetInitiativeStatuses()
        {
            var statuses = await _context.InitiativeStatuses
                .Where(s => !s.IsDelete)
                .ProjectTo<InitiativeStatusDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return statuses;
        }

    }
}
