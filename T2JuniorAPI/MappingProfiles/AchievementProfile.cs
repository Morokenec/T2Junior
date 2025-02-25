using AutoMapper;
using T2JuniorAPI.DTOs.Achievements;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            CreateMap<Achievement, AchievementDTO>()
                .ForMember(dest => dest.MediaPath, opt => opt.MapFrom(src => src.MediaFilesNavigation.Path))
                .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.IdMedia));

            CreateMap<AchievementDTO, Achievement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserAchievement, UserAchievementDTO>();
            CreateMap<UserAchievementDTO, UserAchievement>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.IdAchievement, opt => opt.MapFrom(src => src.AchivementId));

            CreateMap<CreateAchievementDTO, Achievement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<UpdateAchievementDTO, Achievement>();

        }
    }
}
