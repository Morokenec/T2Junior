using AutoMapper;
using T2JuniorAPI.DTOs.MediaTypes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MediaTypeProfile : Profile
    {
        public MediaTypeProfile()
        {
            CreateMap<MediaTypeDTO, MediaType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<MediaType, MediaTypeDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
