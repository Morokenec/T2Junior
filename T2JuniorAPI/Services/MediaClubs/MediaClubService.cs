using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;
using T2JuniorAPI.Services.MediaTypes;

namespace T2JuniorAPI.Services.MediaClubs
{
    public class MediaClubService : IMediaClubService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IMediafileService _mediafileService;

        public MediaClubService(ApplicationDbContext context, IMapper mapper, IMediaTypeService mediaTypeService, UserManager<ApplicationUser> userManager, IMediafileService mediafileService)
        {
            _context = context;
            _mapper = mapper;
            _mediaTypeService = mediaTypeService;
            _userManager = userManager;
            _mediafileService = mediafileService;
        }

        public async Task<MediaClubDTO> AddMediaByClubId(MediafileUploadDTO mediafileUpload, Guid clubId)
        {
            if (!await IsUserAdminInClub(mediafileUpload.IdUser, clubId))
            {
                throw new UnauthorizedAccessException("User is not an admin in the club");
            }

            var mediafile = await _mediafileService.CreateMediafileAsync(mediafileUpload);
            var mediaClub = _mapper.Map<MediaClub>(new MediaClubDTO { IdClub = clubId, IdMedia = mediafile.Id });
            _context.MediaClubs.Add(mediaClub);
            await _context.SaveChangesAsync();
            return _mapper.Map<MediaClubDTO>(mediaClub);
        }

        public async Task<string> SetAvatarForClub(MediafileUploadDTO uploadDTO, Guid clubId)
        {
            if (!await IsUserAdminInClub(uploadDTO.IdUser, clubId))
            {
                throw new UnauthorizedAccessException("User is not an admin in the club");
            }

            var mediafile = await _mediafileService.CreateMediafileAsync(uploadDTO, true);
            var mediaClub = _mapper.Map<MediaClub>(new MediaClubDTO { IdClub = clubId, IdMedia = mediafile.Id, IsAvatar = true });
            _context.MediaClubs.Add(mediaClub);
            await _context.SaveChangesAsync();
            return "Avatar set successfully";
        }

        public async Task<string> DeleteMediaByClubId(Guid clubId, Guid mediaId, Guid userId)
        {
            if (!await IsUserAdminInClub(userId, clubId))
            {
                throw new UnauthorizedAccessException("User is not an admin in the club");
            }

            var mediaClub = await _context.MediaClubs
                .Where(mc => mc.IdClub == clubId && mc.IdMedia == mediaId && !mc.IsDelete)
                .FirstOrDefaultAsync();

            if (mediaClub == null)
                return "Media not found";

            mediaClub.IsDelete = true;
            mediaClub.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            await _mediafileService.DeleteMediaByUserId(new MediafileDeleteDTO { UserId = userId, MediaId = mediaId });

            return "Media deleted successfully";
        }

        public async Task<string> DeleteAvatarFromClub(Guid clubId, Guid mediaId, Guid userId)
        {
            if (!await IsUserAdminInClub(userId, clubId))
            {
                throw new UnauthorizedAccessException("User is not an admin in the club");
            }

            var mediaClub = await _context.MediaClubs
                .Where(mc => mc.IdClub == clubId && mc.IdMedia == mediaId && mc.IsAvatar && !mc.IsDelete)
                .FirstOrDefaultAsync();

            if (mediaClub == null)
                return "Avatar not found";

            mediaClub.IsDelete = true;
            mediaClub.UpdateDate= DateTime.Now;
            await _context.SaveChangesAsync();

            return "Avatar deleted successfully";
        }
        public async Task<IEnumerable<MediaClubDTO>> GetAllMediaByClubId(Guid clubId)
        {
            var mediaClubs = await _context.MediaClubs
                .Include(mc => mc.MediaFilesNavigation.IdMediaTypesNavigation)
                .Where(mc => mc.IdClub == clubId && !mc.IsDelete)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MediaClubDTO>>(mediaClubs);
        }

        public async Task<bool> IsUserAdminInClub(Guid userId, Guid clubId)
        {
            var clubUser = await _context.ClubUsers
                .Include(cu => cu.IdRoleNavigation)
                .FirstOrDefaultAsync(cu => cu.IdUser == userId &&  cu.IdClub == clubId);

            if (clubUser == null) return false;

            return clubUser.IdRoleNavigation.Name == "admin";
        }
    }
}
