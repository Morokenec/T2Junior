using AutoMapper;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.DTOs.MediaTypes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaTypes;

namespace T2JuniorAPI.Services.Medias
{
    public class MediafileService : IMediafileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediaTypeService _mediaTypeService;


        public MediafileService(ApplicationDbContext context, IMapper mapper, IMediaTypeService mediaTypeService)
        {
            _context = context;
            _mapper = mapper;
            _mediaTypeService = mediaTypeService;
        }

        public async Task<Mediafile> UploadMediafileAsync(MediafileUploadDTO uploadDTO)
        {
            try
            {
                var mediafile = _mapper.Map<Mediafile>(uploadDTO);
                mediafile.Path = await SaveFileAsync(uploadDTO.File);
                mediafile.IdType = await GetMediaTypeId(uploadDTO.File.FileName);

                _context.Mediafiles.Add(mediafile);
                await _context.SaveChangesAsync();

                return mediafile;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
            
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine("wwwroot", "uploads");
            
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var path = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        private async Task<Guid> GetMediaTypeId(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return (await _mediaTypeService.GetGuidOrCreateMediaType(new MediaTypeDTO { Name = "Image" })).Id;
                case ".mp4":
                case ".avi":
                case ".mkv":
                    return (await _mediaTypeService.GetGuidOrCreateMediaType(new MediaTypeDTO { Name = "Video" })).Id;

                default:
                    throw new InvalidOperationException("Unsupported file type");
            }
        }
    }
}
