using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Notes;

namespace T2JuniorAPI.Services.NewsFeeds
{
    public class NewsFeedService : INewsFeedService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NewsFeedService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<NoteDTO>> GetNewsFeedAsync(Guid userId)
        {
            var userSubscriptions = await _context.UserSubscribers
                .Where(sub => sub.IdSubscriber == userId)
                .Select(sub => sub.IdUser)
                .ToListAsync();

            var clubSubscriptions = await _context.ClubUsers
                .Where(cu => cu.IdUser == userId)
                .Select(cu => cu.IdClub)
                .ToListAsync();

            var wallIds = await _context.Walls
                .Where(w => (w.IdUserOwner.HasValue && userSubscriptions.Contains(w.IdUserOwner.Value)) ||
                            (w.IdClubOwner.HasValue && clubSubscriptions.Contains(w.IdClubOwner.Value)))
                .Select(w => w.Id)
                .ToListAsync();

            var notes = await _context.Notes
                .Where(n => wallIds.Contains(n.IdWall) && !n.IsDelete)
                .Include(n => n.Likes)
                .Include(n => n.Comments)
                .OrderByDescending(n => n.CreationDate)
                .ToListAsync();

            var noteDTOs = _mapper.Map<IEnumerable<NoteDTO>>(notes);

            foreach (var noteDTO in noteDTOs)
            {
                var note = notes.FirstOrDefault(n => n.Id == noteDTO.Id);
                if (note != null)
                {
                    noteDTO.LikeCount = note.Likes.Count(l => !l.IsDelete);
                    noteDTO.CommentsCount = note.Comments.Count(c => !c.IsDelete);
                }
            }

            return noteDTOs;
        }
    }
}
