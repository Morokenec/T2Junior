using AutoMapper;
using T2JuniorAPI.DTOs.Walls;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class WallProfile : Profile
    {
        public WallProfile()
        {
            CreateMap<Wall, WallDTO>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.IdTypeNavigation.Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.UserOwner != null ? $"{src.UserOwner.FirstName} {src.UserOwner.LastName}" : src.ClubOwner.Name));

            CreateMap<CreateWallDTO, Wall>()
                .ForMember(dest => dest.IdType, opt => opt.MapFrom(src => src.IdType))
                .ForMember(dest => dest.IdOwner, opt => opt.MapFrom(src => src.IdOwner));
        }
    }
}
