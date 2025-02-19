using AutoMapper;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class NoteStatusProfile : Profile
    {
        public NoteStatusProfile()
        {
            CreateMap<NoteStatus, NoteStatusDTO>();
            CreateMap<NoteStatusDTO, NoteStatus>();
        }
    }
}
