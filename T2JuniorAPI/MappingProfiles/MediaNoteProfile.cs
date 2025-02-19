using AutoMapper;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class MediaNoteProfile : Profile
    {
        public MediaNoteProfile()
        {
            CreateMap<MediaNote, MediaNoteDTO>();
            CreateMap<MediaNoteDTO, MediaNote>();
        }
    }
}
