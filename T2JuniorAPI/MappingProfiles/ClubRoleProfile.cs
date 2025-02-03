using AutoMapper;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class ClubRoleProfile : Profile
    {
        public ClubRoleProfile()
        {
            CreateMap<ClubRole, ClubRolesDTO>();
            CreateMap<ClubRolesDTO, ClubRole>();
        }
    }
}
