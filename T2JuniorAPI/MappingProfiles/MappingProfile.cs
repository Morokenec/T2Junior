using AutoMapper;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<UserSubscribers, SubscriberProfileDTO>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser))
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
            ////mapping for subscribers
            //CreateMap<UserSubscribers, SubscriberProfileDTO>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdSubscriber))
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Subscriber.FirstName} {src.Subscriber.LastName}"));

            ////CreateMap<UserSubscribers, SubscriberProfileDTO>()
            ////    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser))
            ////    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
        }
    }
}
