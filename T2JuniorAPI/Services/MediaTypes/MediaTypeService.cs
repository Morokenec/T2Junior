using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.MediaTypes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.MediaTypes
{
    public class MediaTypeService : IMediaTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MediaTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MediaTypeDTO> GetGuidOrCreateMediaType(MediaTypeDTO mediaTypeDTO)
        {
            if (string.IsNullOrWhiteSpace(mediaTypeDTO.Name))
                throw new ApplicationException("Name is reqired");

            var existingMediaType = await _context.MediaTypes
                .FirstOrDefaultAsync(mt => mt.Name == mediaTypeDTO.Name);

            if (existingMediaType != null)
                return _mapper.Map<MediaTypeDTO>(existingMediaType);

            var newType = _mapper.Map<MediaType>(mediaTypeDTO);

            await _context.MediaTypes.AddAsync(newType);
            await _context.SaveChangesAsync();

            return _mapper.Map<MediaTypeDTO>(newType);
        }

        public async Task<List<MediaTypeDTO>> GetAllMediaTypes()
        {
            return await _context.MediaTypes
                .Where(cr => cr.IsDelete == false)
                .ProjectTo<MediaTypeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> DeleteMediaType(Guid id)
        {
            var mediaType = await _context.MediaTypes.FindAsync(id);
            if (mediaType == null)
                return false;

            mediaType.IsDelete = true;
            mediaType.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
