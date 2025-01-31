using AutoMapper;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, ClubPageDTO>()
                .ForMember(dest => dest.Users, opt => opt.Ignore()); // Ignore users because they will be filled in separately 

            CreateMap<Club, ClubProfileDTO>()
                .ForMember(dest => dest.UsersCount, opt => opt.MapFrom(src => src.ClubUsers.Count));

            CreateMap<CreateClubDTO, Club>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id because auto increment
                .ForMember(dest => dest.Raiting, opt => opt.Ignore());

            CreateMap<ClubUser, SubscriberProfileDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.IdUserNavigation.FirstName} {src.IdUserNavigation.LastName}"));
        }
    }
}
