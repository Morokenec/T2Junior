using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.NoteStatuses;

namespace T2JuniorAPI.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly INoteStatusService _noteStatusService;

        public NoteService(IMapper mapper, ApplicationDbContext context, INoteStatusService noteStatusService)
        {
            _mapper = mapper;
            _context = context;
            _noteStatusService = noteStatusService;
        }

        public async Task<NoteDTO> CreateNoteAsync(Guid idOwner, CreateNoteDTO noteDTO)
        {
            var wall = await _context.Walls
                .FirstOrDefaultAsync(w => (w.IdUserOwner == idOwner || w.IdClubOwner == idOwner) & !w.IsDelete);

            if (wall == null)
                throw new ApplicationException("Wall not found for the owner");

            var createdStatus = await _noteStatusService.GetOrCreateNoteStatusAsync("создание");

            var note = _mapper.Map<Note>(noteDTO);
            note.IdWall = wall.Id;
            note.IdStatus = createdStatus.Id;

            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return _mapper.Map<NoteDTO>(note);
        }

        public async Task<NoteDTO> UpdateNoteAsync(Guid IdNote, UpdateNoteDTO noteDTO)
        {
            var note = await _context.Notes.FindAsync(IdNote);

            if (note == null)
                throw new ApplicationException("Note not found");

            _mapper.Map(noteDTO, note);
            await _context.SaveChangesAsync();

            return _mapper.Map<NoteDTO>(note);
        }

        public async Task<bool> DeleteNoteAsync(Guid idNote)
        {
            var note = await _context.Notes.FindAsync(idNote);

            if (note == null)
                throw new ApplicationException("Note not found");

            note.IsDelete = true;
            note.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<NoteDTO> RepostNoteAsync(Guid idNote, Guid idOwner)
        {
            var originalNote = await _context.Notes
                .Include(n => n.IdWallNavigation)
                .FirstOrDefaultAsync(n => n.Id == idNote && !n.IsDelete);

            if (originalNote == null)
                throw new ApplicationException("Original note not found");

            var createNewNoteDTO = new CreateNoteDTO
            {
                Name = originalNote.Name,
                Description = originalNote.Description,
            };

            var newNote = await CreateNoteAsync(idOwner, createNewNoteDTO);

            var repostNote = await _context.Notes.FindAsync(newNote.Id);
            repostNote.IdRepost = idNote;

            await _context.SaveChangesAsync();

            return _mapper.Map<NoteDTO>(repostNote);
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesByIdOwnerAsync(Guid idOwner)
        {
            // Загрузка стены
            var wall = await _context.Walls
                .Include(w => w.Notes)
                    .ThenInclude(n => n.MediaNotes)
                        .ThenInclude(mn => mn.IdMediaNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync(w => (w.IdUserOwner == idOwner || w.IdClubOwner == idOwner) && !w.IsDelete);

            if (wall == null)
                throw new ApplicationException("Wall not found for the owner");

            // Загрузка комментариев к записям
            var noteIds = wall.Notes.Select(n => n.Id).ToList();
            var comments = await _context.Comments
                .Include(c => c.MediaComments)
                    .ThenInclude(mc => mc.IdMediaNavigation)
                .Include(c => c.IdUserNavigation)
                    .ThenInclude(u => u.UserAvatars)
                        .ThenInclude(ua => ua.Media)
                .Include(c => c.CommentLikes)
                .Where(c => noteIds.Contains(c.IdNote) && c.ParrentCommentId == null && !c.IsDelete)
                .ToListAsync();

            // Загрузка подкомментариев
            var commentIds = comments.Select(c => c.Id).ToList();
            var subComments = await _context.Comments
                .Include(c => c.MediaComments)
                    .ThenInclude(mc => mc.IdMediaNavigation)
                .Include(c => c.IdUserNavigation)
                    .ThenInclude(u => u.UserAvatars)
                        .ThenInclude(ua => ua.Media)
                .Include(c => c.CommentLikes)
                .Where(c => commentIds.Contains(c.ParrentCommentId.Value) && !c.IsDelete)
                .ToListAsync();

            // Объединение подкомментариев с комментариями
            foreach (var comment in comments)
            {
                comment.InverseParrentComment = subComments.Where(sc => sc.ParrentCommentId == comment.Id).ToList();
            }

            // Маппинг записей в DTO
            var notes = _mapper.Map<IEnumerable<NoteDTO>>(wall.Notes);
            return notes;
        }

        public async Task<bool> UpdateNoteStatusAsync(Guid idNote, Guid idStatus)
        {
            var note = await _context.Notes.FindAsync(idNote);
            var status = await _context.NoteStatuses.FindAsync(idStatus);

            if (note == null) throw new ApplicationException("Note not found");
            if (status == null) throw new ApplicationException("Status not found");

            note.IdStatus = idStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NoteDTO> ToggleLikeNoteAsync(Guid idNote, Guid userId)
        {
            var note = await _context.Notes
                .Include(n => n.Likes)
                .FirstOrDefaultAsync(n => n.Id == idNote && !n.IsDelete);

            if (note == null)
                throw new ApplicationException("Note Not Found");

            var existingLike = note.Likes.FirstOrDefault(l => l.UserId == userId);

            if (existingLike != null)
            {
                // Delete Like
                if (!existingLike.IsDelete)
                {
                    existingLike.IsDelete = true;
                    existingLike.UpdateDate = DateTime.Now;
                    note.LikeCount--;
                }
                else
                {
                    // Like recovery
                    existingLike.IsDelete= false;
                    existingLike.UpdateDate= DateTime.Now;
                    note.LikeCount++;
                }
            }
            else
            {
                // add new Like
                note.Likes.Add(new NoteLike { UserId = userId });
                note.LikeCount++;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<NoteDTO>(note);
        }
    }
}
