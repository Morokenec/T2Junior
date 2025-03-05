using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.Achievements
{
    /// <summary>
    /// Сервис для работы с достижениями, он позволяет получать списки достижений для определенного пользователя, создавать, обновлять, удалять и активировать/деактивировать достижения.
    /// </summary>
    public class AchievementService : IAchievementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileservice;

        /// <summary>
        /// Функциональность для работы с достижениями.
        /// </summary>
        /// <param name="mediafileservice">Работа с медиафайлами.</param>
        /// <param name="mapper"> отображение объектов из одного типа на другой.</param>
        /// <param name="context">Контекст базы данных.</param>
        public AchievementService(IMediafileService mediafileservice, IMapper mapper, ApplicationDbContext context)
        {
            _mediafileservice = mediafileservice;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Список всех достижений для указанного пользователя.
        /// </summary>
        /// <param name="userId">Для получения списка всех достижений.</param>
        public async Task<List<AchievementDTO>> GetAchievementsAllByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid User ID");

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

        /// <summary>
        /// Список достижений, которые были получены указанным пользователем.
        /// </summary>
        /// <param name="userId">Для получения списка достижений, которые были получены указанным пользователем.</param>
        public async Task<List<AchievementDTO>> GetAchievementsByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user ID.");

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

        /// <summary>
        /// Создание новых достижений на основе данных.
        /// </summary>
        /// <param name="achievementDTO">данные о новом достижении.</param>
        /// <param name="uploadDTO">Данные о загружаемом медиафайле.</param>
        public async Task<AchievementDTO> CreateAchievementAsync(CreateAchievementDTO achievementDTO, MediafileUploadDTO uploadDTO)
        {
            if (achievementDTO == null)
                throw new ArgumentNullException(nameof(achievementDTO), "Achievement data cannot be null.");

            if (string.IsNullOrWhiteSpace(achievementDTO.Name))
                throw new ArgumentException("Achievement name cannot be empty.", nameof(achievementDTO.Name));

            var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO, true);
            var achivement = _mapper.Map<Achievement>(achievementDTO);
            achivement.IdMedia = mediafile.Id;

            _context.Achievements.Add(achivement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AchievementDTO>(achivement);
        }

        /// <summary>
        /// Создание новых достижений на основе данных.
        /// </summary>
        /// <param name="achievementDTO">данные о новом достижении.</param>
        /// <param name="uploadDTO">Данные о загружаемом медиафайле.</param>
        public async Task<AchievementDTO> UpdateAchievementAsync(UpdateAchievementDTO achievementDTO, MediafileUploadDTO uploadDTO = null)
        {
            if (achievementDTO == null)
                throw new ArgumentNullException(nameof(achievementDTO), "Achievement data cannot be null.");

            if (achievementDTO.Id == Guid.Empty)
                throw new ArgumentException("Invalid achievement ID.", nameof(achievementDTO.Id));

            var existingAchievement = await _context.Achievements.Include(a => a.MediaFilesNavigation).SingleOrDefaultAsync(a => a.Id == achievementDTO.Id);
            if (existingAchievement == null)
                throw new ArgumentException("Achievement not found.");

            if (uploadDTO != null)
            {
                var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO, true);
                existingAchievement.IdMedia = mediafile.Id;
            }
            _mapper.Map(achievementDTO, existingAchievement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AchievementDTO>(existingAchievement);
        }

        /// <summary>
        /// Удаление достижений из базы данных.
        /// </summary>
        /// <param name="achievementId">Данные об удаляемом достижении.</param>
        public async Task DeleteAchievementAsync(Guid achievementId)
        {
            if (achievementId == Guid.Empty)
                throw new ArgumentException("Invalid achievement ID.");

            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && !achivement.IsDelete)
            {
                achivement.IsDelete = true;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Активация достижения.
        /// </summary>
        /// <param name="achievementId">Данные об активированном достижении.</param>
        public async Task ActivateAchievementAsync(Guid achievementId)
        {
            if (achievementId == Guid.Empty)
                throw new ArgumentException("Invalid achievement ID.");

            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && !achivement.IsActive && !achivement.IsDelete)
            {
                achivement.IsActive = true;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Деактивация достижения.
        /// </summary>
        /// <param name="achievementId">Данные об деактивированном достижении.</param>
        public async Task DeactivateAchievementAsync(Guid achievementId)
        {
            if (achievementId == Guid.Empty)
                throw new ArgumentException("Invalid achievement ID.");

            var achivement = await _context.Achievements.FindAsync(achievementId);
            if (achivement != null && achivement.IsActive && !achivement.IsDelete)
            {
                achivement.IsActive = false;
                achivement.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Добавление достижения конкретному пользователю.
        /// </summary>
        /// <param name="userId">Пользователь</param>
        /// <param name="achievementId">Достижение, присваемое пользователю</param>
        public async Task AssignAchievementToUserAsync(Guid userId, Guid achievementId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user ID.");

            if (achievementId == Guid.Empty)
                throw new ArgumentException("Invalid achievement ID.");

            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new ApplicationException("User not found.");

            var achievementExists = await _context.Achievements.AnyAsync(a => a.Id == achievementId && !a.IsDelete);
            if (!achievementExists)
                throw new ApplicationException("Achievement not found.");

            var userAchivement = _mapper.Map<UserAchievement>(new UserAchievementDTO { UserId = userId, AchivementId = achievementId });
            _context.UserAchievements.Add(userAchivement);
            await _context.SaveChangesAsync();
        }
    }
}
