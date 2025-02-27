using AutoMapper;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class InitiativeProfile : Profile
    {
        public InitiativeProfile()
        {
            CreateMap<InitiativeDTO, Initiative>().ReverseMap();
            CreateMap<InitiativeCommentDTO, InitiativeComment>().ReverseMap();
        }
    }
}
