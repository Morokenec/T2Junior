using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.Achievements
{
    public class AchievementService : IAchievementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileservice;

        public AchievementService(IMediafileService mediafileservice, IMapper mapper, ApplicationDbContext context)
        {
            _mediafileservice = mediafileservice;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<AchievementDTO>> GetAchievementsAllByUserIdAsync(Guid userId)
        {
            var achivements = await _context.Achievements
                .Where(a => !a.IsDelete)
                .Include(a => a.MediaFilesNavigation)
                .ToListAsync();

            var userAchivements = await _context.UserAchievements
                .Where(ua => ua.IdUser == userId)
                .Select(ua => ua.IdAchievement)
                .ToListAsync();

            var achivementsDto = _mapper.Map<List<AchievementDTO>>(achivements);
            foreach (var achivement in achivementsDto)
            {
                achivement.IsRecived = userAchivements.Contains(achivement.Id);
            }

            return achivementsDto;
        }

        public async Task<List<AchievementDTO>> GetAchievementsByUserIdAsync(Guid userId)
        {
            var achivements = await _context.UserAchievements
                .Where(ua => ua.IdUser == userId && !ua.AchievementsNavigation.IsDelete && !ua.IsDelete)
                .Include(ua => ua.AchievementsNavigation)
                .ThenInclude(a => a.MediaFilesNavigation)
                .Select(ua => ua.AchievementsNavigation)
                .ToListAsync();

            var achievementsDto = _mapper.Map<List<AchievementDTO>>(achivements);
            foreach (var achievement in achievementsDto)
            {
                achievement.IsRecived = true;
            }

            return achievementsDto;
        }
        public async Task<AchievementDTO> CreateAchievementAsync(CreateAchievementDTO achievementDTO, MediafileUploadDTO uploadDTO)
        {
            if (achievementDTO == null || string.IsNullOrWhiteSpace(achievementDTO.Name)) 
                throw new ArgumentException("Inavlid achivement data");

            var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO, true);
            var achivement = _mapper.Map<Achievement>(achievementDTO);
            achivement.IdMedia = mediafile.Id;

            _context.Achievements.Add(achivement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AchievementDTO>(achivement);
        }
        public async Task<AchievementDTO> UpdateAchievementAsync(UpdateAchievementDTO achievementDTO, MediafileUploadDTO uploadDTO = null)
        {
            var existingAchivement = await _context.Achievements.Include(a => a.MediaFilesNavigation).SingleOrDefaultAsync(a => a.Id == achievementDTO.Id);
            if (existingAchivement == null)
                throw new ArgumentException("Achievement not found."); 

            if (uploadDTO != null)
            {
                var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO, true);
                existingAchivement.IdMedia = mediafile.Id;
            }
            _mapper.Map(achievementDTO, existingAchivement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AchievementDTO>(existingAchivement);
        }

        public async Task DeleteAchievementAsync(Guid achievementId)
        {
            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && !achivement.IsDelete)
            {
                achivement.IsDelete = true;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivateAchievementAsync(Guid achievementId)
        {
            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && !achivement.IsActive && !achivement.IsDelete)
            {
                achivement.IsActive = true;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateAchievementAsync(Guid achievementId)
        {
            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && achivement.IsActive && !achivement.IsDelete)
            {
                achivement.IsActive = false;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignAchievementToUserAsync(Guid userId, Guid achievementId)
        {
            var userAchivement = _mapper.Map<UserAchievement>(new UserAchievementDTO { UserId = userId, AchivementId = achievementId });
            _context.UserAchievements.Add(userAchivement);
            await _context.SaveChangesAsync();
        }
    }
}
