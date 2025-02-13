using AutoMapper;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MediaClubProfile : Profile
    {
        public MediaClubProfile()
        {
            CreateMap<MediaClub, MediaClubDTO>()
                .ForMember(dest => dest.IdClub, opt => opt.MapFrom(src => src.IdClub))
                .ForMember(dest => dest.IdMedia, opt => opt.MapFrom(src => src.IdMedia))
                .ForMember(dest => dest.IsAvatar, opt => opt.MapFrom(src => src.IsAvatar))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.MediaFilesNavigation.IdMediaTypesNavigation.Name))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.MediaFilesNavigation.Path));

            CreateMap<MediaClubDTO, MediaClub>()
                .ForMember(dest => dest.IdClub, opt => opt.MapFrom(src => src.IdClub))
                .ForMember(dest => dest.IdMedia, opt => opt.MapFrom(src => src.IdMedia))
                .ForMember(dest => dest.IsAvatar, opt => opt.MapFrom(src => src.IsAvatar));
        }
    }
}
