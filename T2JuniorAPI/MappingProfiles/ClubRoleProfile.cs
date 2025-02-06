using AutoMapper;
using T2JuniorAPI.DTOs.ClubRoles;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class ClubRoleProfile : Profile
    {
        public ClubRoleProfile()
        {
            CreateMap<ClubRole, ClubRolesDTO>();
            CreateMap<ClubRolesDTO, ClubRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
