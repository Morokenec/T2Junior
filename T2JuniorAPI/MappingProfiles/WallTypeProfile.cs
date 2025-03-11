using AutoMapper;
using T2JuniorAPI.DTOs.WallTypes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class WallTypeProfile : Profile
    {
        public WallTypeProfile()
        {
            CreateMap<WallType, WallTypeDTO>();
            CreateMap<CreateWallTypeDTO, WallType>();
        }
    }
}
