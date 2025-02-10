using AutoMapper;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<MediafileUploadDTO, Mediafile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdUserNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdMediaTypesNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MediaComment, opt => opt.Ignore())
                .ForMember(dest => dest.MediaNote, opt => opt.Ignore())
                .ForMember(dest => dest.Achievements, opt => opt.Ignore())
                .ForMember(dest => dest.MediaEvents, opt => opt.Ignore())
                .ForMember(dest => dest.MediaClubs, opt => opt.Ignore());

            CreateMap<Mediafile, MediafileDTO>()
                .ForMember(dest => dest.MediaTypeName, opt => opt.MapFrom(src => src.IdMediaTypesNavigation.Name));
        }
    }
}
