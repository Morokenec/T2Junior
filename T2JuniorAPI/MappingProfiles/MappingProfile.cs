using AutoMapper;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubscribeUserDTO, UserSubscribers>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IdSubscriber, opt => opt.MapFrom(src => src.SubscriberId));
            CreateMap<UnsubscribeUserDTO, UserSubscribers>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.SubscriptionId))
                .ForMember(dest => dest.IdSubscriber, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
