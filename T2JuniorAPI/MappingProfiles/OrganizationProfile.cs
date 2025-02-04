using AutoMapper;
using T2JuniorAPI.DTOs;

namespace T2JuniorAPI.MappingProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<OrganizationDto, Organization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false));
        }
    }
}
