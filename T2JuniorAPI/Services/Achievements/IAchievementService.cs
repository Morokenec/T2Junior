using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Achievements
{
    public interface IAchievementService
    {
        Task<List<AchievementDTO>> GetAchievementsAllByUserIdAsync(Guid userId);
        Task<List<AchievementDTO>> GetAchievementsByUserIdAsync(Guid userId);
        Task<AchievementDTO> CreateAchievementAsync(CreateAchievementDTO achievementDto, MediafileUploadDTO uploadDTO);
        Task<AchievementDTO> UpdateAchievementAsync(UpdateAchievementDTO achievementDto, MediafileUploadDTO uploadDTO = null);
        Task DeleteAchievementAsync(Guid achievementId);
        Task ActivateAchievementAsync(Guid achievementId);
        Task DeactivateAchievementAsync(Guid achievementId);
        Task AssignAchievementToUserAsync(Guid userId, Guid achievementId);
    }
}
