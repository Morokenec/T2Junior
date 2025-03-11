using AutoMapper;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MediaEventProfile : Profile
    {
        public MediaEventProfile()
        {
            CreateMap<MediaEvent, MediaEventDTO>()
                .ForMember(dest => dest.IdMedia, opt => opt.MapFrom(src => src.IdMedia))
                .ForMember(dest => dest.IdEvent, opt => opt.MapFrom(src => src.IdEvent))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.MediaFilesNavigation.Path));
            
            CreateMap<MediaEventDTO, MediaEvent>()
                .ForMember(dest => dest.IdMedia, opt => opt.MapFrom(src => src.IdMedia))
                .ForMember(dest => dest.IdEvent, opt => opt.MapFrom(src => src.IdEvent));
        }
    }
}
