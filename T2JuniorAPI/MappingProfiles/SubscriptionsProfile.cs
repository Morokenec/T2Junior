using AutoMapper;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class SubscriptionsProfile : Profile
    {
        public SubscriptionsProfile()
        {
            CreateMap<UserSubscribers, SubscriberProfileDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser))
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
        }
    }
}
