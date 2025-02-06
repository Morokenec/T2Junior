using T2JuniorAPI.DTOs;

namespace T2JuniorAPI.Services
{
    public interface IEventService
    {
        Task<List<EventCalendarDTO>> GetUserCalendar(Guid userId, int month, int year);
        Task<List<EventCalendarDTO>> GetClubCalendar(Guid clubId, int month, int year);
        Task<Guid> CreateEvent(CreateEventDTO createEventDto);
        Task DeleteEvent(Guid eventId);
        Task PutEvent(UpdateEventDTO updateEventDto);
        Task<EventDTO> GetEventById(Guid eventId);

        Task<List<EventDirectionDTO>> GetEventDirection();
        Task<Guid> CreateEventDirection(EventDirectionDTO createEventDirectionDto);
        Task DeleteEventDirection(Guid directionId);
        Task PutEventDirection(EventDirectionDTO updateEventDirectionDto);
    }
}
