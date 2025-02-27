using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Initiatives;
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
        public async Task<Initiative> CreateInitiativeAsync(InitiativeDTO initiativeDto)
        {
            var initiative = _mapper.Map<Initiative>(initiativeDto);
            _context.Initiatives.Add(initiative);
            await _context.SaveChangesAsync();
            return initiative;
        }

        public async Task<Initiative> UpdateInitiativeAsync(Guid id, InitiativeDTO initiativeDto)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return null;

            _mapper.Map(initiativeDto, initiative);
            await _context.SaveChangesAsync();
            return initiative;
        }

        public async Task<bool> DeleteInitiativeAsync(Guid id)
        {
            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null) return false;

            _context.Initiatives.Remove(initiative);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Initiative>> GetAllInitiativesAsync()
        {
            return await _context.Initiatives.ToListAsync();
        }

        public async Task<Initiative> GetInitiativeByIdAsync(Guid id)
        {
            return await _context.Initiatives.FindAsync(id);
        }

        public async Task<bool> VoteForInitiativeAsync(Guid id, Guid userId)
        {
            var vote = new Vote { IdInitiative = id, IdUser = userId };
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CommentOnInitiativeAsync(Guid id, InitiativeCommentDTO commentDto)
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

        public async Task<IEnumerable<Initiative>> GetInitiativesWithRatingAsync()
        {
            var initiatives = await _context.Initiatives
                .Include(i => i.Votes)
                .Include(i => i.InitiativeComments)
                .ToListAsync();

            var initiativesWithRating = initiatives
                .OrderByDescending(i => i.Votes.Count + i.InitiativeComments.Count)
                .ToList();

            return initiativesWithRating;
        }

    }
}
