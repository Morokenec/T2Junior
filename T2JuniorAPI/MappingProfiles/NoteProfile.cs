using AutoMapper;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDTO>();
            CreateMap<CreateNoteDTO, Note>();
            CreateMap<UpdateNoteDTO, Note>();
        }
    }
}
