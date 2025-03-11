using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Events;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaClubs;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.Events
{
    /// <summary>
    /// Сервис для работы с событиями.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediaClubService _mediaClubService;
        private readonly IMediafileService _mediafileService;

        /// <summary>
        /// Инициализация нового класса.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="mapper">Маппер для преобразования объектов.</param>
        /// <param name="mediaClubService">Сервис для работы с медиаклубами.</param>
        /// <param name="mediafileService">Сервис для работы с медиафайлами.</param>
        public EventService(ApplicationDbContext context, IMapper mapper, IMediaClubService mediaClubService, IMediafileService mediafileService)
        {
            _context = context;
            _mapper = mapper;
            _mediaClubService = mediaClubService;
            _mediafileService = mediafileService;
        }

        /// <summary>
        /// Возвращение календаря событий для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="month">Месяц.</param>
        /// <param name="year">Год.</param>
        /// <returns>Список событий.</returns>
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

        /// <summary>
        /// Возвращение календаря событий для указанного медиаклуба.
        /// </summary>
        /// <param name="clubId">Идентификатор медиаклуба.</param>
        /// <param name="month">Месяц.</param>
        /// <param name="year">Год.</param>
        /// <returns>Список событий.</returns>
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

        /// <summary>
        /// Создание события.
        /// </summary>
        /// <param name="createEventDto">Данные для создания события.</param>
        /// <param name="uploadDTO">Данные для загрузки медиафайла.</param>
        /// <returns>Идентификатор созданного события.</returns>
        public async Task<Guid> CreateEvent(CreateEventDTO createEventDto, MediafileUploadDTO uploadDTO = null)
        {
            try
            {
                // Проверка существования Club
                var clubExists = await _context.Clubs.AnyAsync(c => c.Id == createEventDto.IdClub);
                if (!clubExists)
                    throw new InvalidOperationException("Club with the specified IdClub does not exist.");

                // Проверка существования EventDirection
                var directionExists = await _context.EventDirections.AnyAsync(d => d.Id == createEventDto.IdDirection);
                if (!directionExists)
                    throw new InvalidOperationException("EventDirection with the specified IdDirection does not exist.");

                // проверка что пользователь является админом клуба
                if (uploadDTO != null)
                    if (!await _mediaClubService.IsUserAdminInClub(uploadDTO.IdUser, createEventDto.IdClub))
                        throw new UnauthorizedAccessException("User is not an admin in the club");

                var mediafile = await _mediafileService.CreateMediafileAsync(uploadDTO, true);
                var newEvent = _mapper.Map<Event>(createEventDto);

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                var mediaEvent = _mapper.Map<MediaEvent>(new MediaEventDTO { IdEvent = newEvent.Id, IdMedia = mediafile.Id });
                _context.MediaEvents.Add(mediaEvent);

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


        /// <summary>
        /// Удаление события по идентификатору.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Задача, которая удаляет событие.</returns>
        public async Task DeleteEvent(Guid eventId)
        {
            var eventToDelete = await _context.Events.Where(e => e.Id == eventId && !e.IsDelete).FirstOrDefaultAsync();
            if (eventToDelete != null)
            {
                var mediaEvent = await _context.MediaEvents.Where(me => me.IdEvent == eventId).FirstOrDefaultAsync();
                if (mediaEvent != null)
                {
                    var media = await _context.Mediafiles.Where(m => m.Id == mediaEvent.IdMedia).FirstOrDefaultAsync();
                    if (media != null)
                    {
                        media.IsDelete = true;
                        media.UpdateDate = DateTime.Now;
                        mediaEvent.IsDelete = true;
                        mediaEvent.UpdateDate = DateTime.Now;
                    }
                }
                eventToDelete.IsDelete = true;
                eventToDelete.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Обновление существующего событие.
        /// </summary>
        /// <param name="updateEventDTO">Данные для обновления события.</param>
        /// <returns>Задача, которая обновляет событие.</returns>
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


        /// <summary>
        /// Возвращение детальной информации о событии по идентификатору.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Задача, которая возвращает детальную информацию о событии.</returns>
        public async Task<EventDTO> GetEventById(Guid eventId)
        {
            var eventDetail = await _context.Events
                .Where(e => e.Id == eventId && !e.IsDelete)
                .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return eventDetail;
        }


        /// <summary>
        /// Возвращение списка всех направлений событий.
        /// </summary>
        /// <returns>Задача, которая возвращает список всех направлений событий.</returns>
        public async Task<List<EventDirectionDTO>> GetEventDirection()
        {
            var directions = await _context.EventDirections
                .Where(ed => !ed.IsDelete)
                .ProjectTo<EventDirectionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return directions;
        }

        /// <summary>
        /// Создание нового направления события.
        /// </summary>
        /// <param name="createEventDirectionDto">Данные для создания направления события.</param>
        /// <returns>Задача, которая создает новое направление события.</returns>
        public async Task<Guid> CreateEventDirection(EventDirectionDTO createEventDirectionDto)
        {
            var newDirection = _mapper.Map<EventDirection>(createEventDirectionDto);

            _context.EventDirections.Add(newDirection);
            await _context.SaveChangesAsync();

            return newDirection.Id;
        }

        /// <summary>
        /// Удаление направления события по идентификатору.
        /// </summary>
        /// <param name="directionId">Идентификатор направления события.</param>
        /// <returns>Задача, которая удаляет направление события.</returns>
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

        /// <summary>
        /// Обновление существующего направления события.
        /// </summary>
        /// <param name="updateEventDirectionDto">Данные для обновления направления события.</param>
        /// <returns>Задача, которая обновляет направление события.</returns>
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
