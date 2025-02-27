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
    public class InitiativeService : IInitiativeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InitiativeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
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

        public async Task<InitiativeOutputDTO> UpdateInitiativeAsync(Guid id, InitiativeInputDTO initiativeDto)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return null;

            _mapper.Map(initiativeDto, initiative);
            await _context.SaveChangesAsync();
            return _mapper.Map<InitiativeOutputDTO>(initiative); 
        }

        public async Task<bool> DeleteInitiativeAsync(Guid id)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return false;

            initiative.IsDelete = true;
            initiative.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

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

        public async Task<bool> CommentOnInitiativeAsync(Guid id, CreateInitiativeComment commentDto)
        {
            var comment = _mapper.Map<InitiativeComment>(commentDto);
            comment.IdInitiative = id;

            _context.InitiativeComments.Add(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeInitiativeStatusAsync(Guid id, Guid statusId)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return false;

            initiative.IdStatus = statusId;
            await _context.SaveChangesAsync();
            return true;
        }

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
