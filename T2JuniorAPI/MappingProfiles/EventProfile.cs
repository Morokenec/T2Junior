using AutoMapper;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventCalendarDTO>()
                .ForMember(dest => dest.IdEvent, opt => opt.MapFrom(src => src.Id));
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.IdEvent, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.IdClub, opt => opt.MapFrom(src => src.IdClub));
            CreateMap<EventDirection, EventDirectionDTO>()
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateEventDTO, Event>()
                .ForMember(dest => dest.IdClub, opt => opt.MapFrom(src => src.IdClub))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection));
            CreateMap<UpdateEventDTO, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEvent));
            CreateMap<EventDirectionDTO, EventDirection>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDirection));

        }
    }
}
