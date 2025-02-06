using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Events
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EventCalendarDTO>> GetUserCalendar(Guid userId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var events = await _context.Events
                .Where(e => e.StartDatetime >= startDate && e.EndDatetime <= endDate && !e.IsDelete)
                .Join(_context.Clubs,
                    e => e.IdClub,
                    c => c.Id,
                    (e, c) => new { Event = e, club = c })
                .Join(_context.ClubUsers,
                    ec => ec.club.Id,
                    cu => cu.IdClub,
                    (ec, cu) => new { ec.Event, cu.IdUser })
                .Where(x => x.IdUser == userId)
                .Select(x => x.Event)
                .ProjectTo<EventCalendarDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }

        public async Task<List<EventCalendarDTO>> GetClubCalendar(Guid clubId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var events = await _context.Events
                .Where(e => e.IdClub == clubId && e.StartDatetime >= startDate && e.EndDatetime <= endDate && !e.IsDelete)
                .ProjectTo<EventCalendarDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }

        public async Task<Guid> CreateEvent(CreateEventDTO createEventDto)
        {
            try
            {
                // Проверка существования Club
                var clubExists = await _context.Clubs.AnyAsync(c => c.Id == createEventDto.IdClub);
                if (!clubExists)
                {
                    throw new InvalidOperationException("Club with the specified IdClub does not exist.");
                }

                // Проверка существования EventDirection
                var directionExists = await _context.EventDirections.AnyAsync(d => d.Id == createEventDto.IdDirection);
                if (!directionExists)
                {
                    throw new InvalidOperationException("EventDirection with the specified IdDirection does not exist.");
                }

                var newEvent = _mapper.Map<Event>(createEventDto);

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return newEvent.Id;
            }
            catch (DbUpdateException ex)
            {
                // Логирование внутреннего исключения
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
                throw new InvalidOperationException("An error occurred while saving the entity changes.", ex);
            }
        }


        public async Task DeleteEvent(Guid eventId)
        {
            var eventToDelete = await _context.Events.FindAsync(eventId);
            if (eventToDelete != null)
            {
                eventToDelete.IsDelete = true;
                eventToDelete.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task PutEvent(UpdateEventDTO updateEventDTO)
        {
            var eventToUpdate = await _context.Events.FindAsync(updateEventDTO.IdEvent);
            if (eventToUpdate != null)
            {
                _mapper.Map(updateEventDTO, eventToUpdate);
                eventToUpdate.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EventDTO> GetEventById(Guid eventId)
        {
            var eventDetail = await _context.Events
                .Where(e => e.Id == eventId && !e.IsDelete)
                .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return eventDetail;
        }
        public async Task<List<EventDirectionDTO>> GetEventDirection()
        {
            var directions = await _context.EventDirections
                .Where(ed => !ed.IsDelete)
                .ProjectTo<EventDirectionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return directions;
        }
        public async Task<Guid> CreateEventDirection(EventDirectionDTO createEventDirectionDto)
        {
            var newDirection = _mapper.Map<EventDirection>(createEventDirectionDto);

            _context.EventDirections.Add(newDirection);
            await _context.SaveChangesAsync();

            return newDirection.Id;
        }

        public async Task DeleteEventDirection(Guid directionId)
        {
            var directionToDelete = await _context.EventDirections.FindAsync(directionId);
            if (directionToDelete != null)
            {
                directionToDelete.IsDelete = true;
                directionToDelete.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task PutEventDirection(EventDirectionDTO updateEventDirectionDto)
        {
            var directionToUpdate = await _context.EventDirections.FindAsync(updateEventDirectionDto.IdDirection);
            if (directionToUpdate != null)
            {
                _mapper.Map(updateEventDirectionDto, directionToUpdate);
                directionToUpdate.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
