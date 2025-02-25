using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.MediaNotes
{
    public class MediaNoteService : IMediaNoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileservice;

        public MediaNoteService(IMapper mapper, ApplicationDbContext context, IMediafileService mediafileservice)
        {
            _mapper = mapper;
            _context = context;
            _mediafileservice = mediafileservice;
        }

        public async Task<MediaNoteDTO> AddMediaToNoteAsync(Guid idNote, MediafileUploadDTO uploadDTO)
        {
            var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO);

            var mediaNote = new MediaNote { IdNote = idNote, IdMedia = mediafile.Id };

            await _context.MediaNotes.AddAsync(mediaNote);
            await _context.SaveChangesAsync();

            return _mapper.Map<MediaNoteDTO>(mediaNote);
        }

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
