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

        public async Task<NoteDTO> RepostNoteAsync(Guid idNote)
        {
            var originalNote = await _context.Notes.FindAsync(idNote);

            if (originalNote == null)
                throw new ApplicationException("Original note not found");

            var repostNote = _mapper.Map<Note>(originalNote);
            repostNote.Id = Guid.NewGuid();
            repostNote.IdRepost = originalNote.Id;

            await _context.Notes.AddAsync(repostNote);
            await _context.SaveChangesAsync();

            return _mapper.Map<NoteDTO>(repostNote);
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesByIdOwnerAsync(Guid idOwner)
        {
            var wall = await _context.Walls
                .Include(w => w.Notes)
                .FirstOrDefaultAsync(w => (w.IdUserOwner == idOwner || w.IdClubOwner == idOwner) & !w.IsDelete);

            if (wall == null) throw new ApplicationException("Wall not found for the owner");

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
    }
}
