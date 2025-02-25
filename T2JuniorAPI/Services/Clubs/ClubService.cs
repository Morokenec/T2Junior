using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Clubs;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Walls;

namespace T2JuniorAPI.Services.Clubs
{
    public class ClubService : IClubService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWallService _wallService;

        public ClubService(ApplicationDbContext context, IMapper mapper, IWallService wallService)
        {
            _context = context;
            _mapper = mapper;
            _wallService = wallService;
        }

        /// <summary>
        /// Возвращает список всех неудалённых клубов с их медиафайлами.
        /// </summary>
        /// <returns>Список DTO-объектов клубов.</returns>
        public async Task<List<AllClubsDTO>> GetAllClubs()
        {
            var clubs = await _context.Clubs
                .Include(c => c.MediaClubs)
                    .ThenInclude(mc => mc.MediaFilesNavigation)
                .Where(c => c.IsDelete == false)
                .ProjectTo<AllClubsDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return clubs;
        }

        /// <summary>
        /// Возвращает информацию о клубе по уникальному идентификатору.
        /// </summary>
        /// <param name="clubId">Уникальный идентификатор клуба.</param>
        /// <returns>DTO-объект клуба или null, если клуб не найден.</returns>
        public async Task<ClubPageDTO> GetClubInfoById(Guid clubId)
        {
            var club = await _context.Clubs
                .Include(c => c.MediaClubs)
                    .ThenInclude(mc => mc.MediaFilesNavigation)
                .Include(c => c.ClubUsers)
                    .ThenInclude(cu => cu.IdUserNavigation)
                        .ThenInclude(u => u.UserAvatars)
                            .ThenInclude(ua => ua.Media)
                .Where(c => c.Id == clubId && !c.IsDelete)
                .ProjectTo<ClubPageDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (club == null)
                return null;

            // Получаем пользователей клуба
            var users = await _context.ClubUsers
                .Where(cu => cu.IdClub == clubId && !cu.IsDelete)
                .ProjectTo<SubscriberProfileDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            //// Заполняем список пользователей в клубе
            club.Users = users;

            return club;
        }

        /// <summary>
        /// Создаёт новый клуб.
        /// </summary>
        /// <param name="club">DTO-объект с новыми данными для нового клуба.</param>
        /// <returns>Строка с результатом создания клуба.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если клуб с таким именем уже существует.</exception>
        public async Task<string> CreateClub(CreateClubDTO club)
        {
            var newClub = _mapper.Map<Club>(club);

            await _context.Clubs.AddAsync(newClub);
            await _context.SaveChangesAsync();

            var role = await _context.ClubRoles.FirstOrDefaultAsync(r => r.Name == "admin");
            if (role == null)
            {
                role = new ClubRole { Name = "admin" };
                await _context.ClubRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            var clubUser = _mapper.Map<ClubUser>(new ClubUserDTO{ IdClub = newClub.Id, IdUser = club.CreatorUserId, IdRole = role.Id });

            await _context.ClubUsers.AddAsync(clubUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClubExists(newClub.Id))
                {
                    return "This club alredy exists";
                }
                else
                {
                    throw;
                }
            }

            await _wallService.CreateWallAsync(newClub.Id);

            return "Successful create club";
        }

        /// <summary>
        /// Проверяет, существует ли клуб с указанным идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор клуба.</param>
        /// <returns>`true`, если клуб существует, иначе `false`.</returns>
        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }

        /// <summary>
        /// Добавляет пользователя в клуб.
        /// </summary>
        /// <param name="clubId">Уникальный идентификатор клуба.</param>
        /// <param name="user">DTO-объект с данными о пользователе.</param>
        /// <returns>Строка с результатом добавления пользователя в клуб.</returns>
        public async Task<string> AddUserToClub(Guid clubId, AddUserToClubDTO user)
        {
            var club = await _context.Clubs.FindAsync(clubId);
            if (club == null)
                return null;

            var clubUser = _mapper.Map<ClubUser>(user);
            clubUser.IdClub = clubId;
            clubUser.UpdateDate = DateTime.Now;

            await _context.ClubUsers.AddAsync(clubUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "User alredy exist in the club";
            }

            return "User successfully added to the club";
        }

        /// <summary>
        /// Удаляет пользователя из клуба.
        /// </summary>
        /// <param name="clubId">Уникальный идентификатор клуба.</param>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>Строка с результатом удаления пользователя из клуба.</returns>
        public async Task<string> DeleteUserFromClub(Guid clubId, Guid userId)
        {
            var clubUser = await _context.ClubUsers
                .FirstOrDefaultAsync(cu => cu.IdClub == clubId && cu.IdUser == userId);

            if (clubUser == null)
                return "User not found in the club";

            clubUser.IsDelete = true;
            clubUser.UpdateDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return $"An error occurred while deleting the user from the club: {ex.Message}";
            }

            return "User successfully deleted from the club";
        }

        /// <summary>
        /// Возвращает профиль клуба по уникальному идентификатору.
        /// </summary>
        /// <param name="clubId">Уникальный идентификатор клуба.</param>
        /// <returns>DTO-объект профиля клуба или `null`, если клуб не найден.</returns>
        public async Task<ClubProfileDTO> GetClubProfileById(Guid clubId)
        {
            var club = await _context.Clubs
                .Include(c => c.MediaClubs)
                    .ThenInclude(mc => mc.MediaFilesNavigation)
                .Where(c => c.Id == clubId && !c.IsDelete)
                .ProjectTo<ClubProfileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (club == null)
                return null;

            club.UsersCount = await _context.ClubUsers.CountAsync(cu => cu.IdClub == clubId && !cu.IsDelete);

            return club;
        }

        /// <summary>
        /// Возвращает список клубов, в которые подписан пользователь.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>Список DTO-объектов клубов.</returns>
        public async Task<List<AllClubsDTO>> GetAllClubsByUserId(Guid userId)
        {
            var clubs = await _context.Clubs
                .Include(c => c.MediaClubs)
                    .ThenInclude(mc => mc.MediaFilesNavigation)
                .Where(c => c.IsDelete == false)
                .ProjectTo<AllClubsDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var userClubsIds = await _context.ClubUsers
                .Where(cu => cu.IdUser == userId)
                .Select(cu => cu.IdClub)
                .ToListAsync();

            foreach (var club in clubs)
            {
                club.IsSubscribe = userClubsIds.Contains(club.Id);
            }

            return clubs;
        }

        /// <summary>
        /// Обновляет информацию о клубе.
        /// </summary>
        /// <param name="clubId">Уникальный идентификатор клуба.</param>
        /// <param name="updateClubDTO">DTO-объект с новыми данными для обновления.</param>
        /// <returns>Строка с результатом обновления.</returns>
        public async Task<string> UpdateClub(Guid clubId, UpdateClubDTO updateClubDTO)
        {
            var club = await _context.Clubs.FindAsync(clubId);
            if (club == null)
                return null;

            _mapper.Map(updateClubDTO, club);
            club.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return "Club updated successfully";
        }

        /// <summary>
        /// Удаляет клуб.
        /// </summary>
        /// <param name="id">Уникальный идентификатор клуба.</param>
        /// <returns>Строка с результатом удаления.</returns>
        public async Task<string> DeleteClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
                return null;

            club.IsDelete = true;
            club.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            await _wallService.DeleteWallAsync(id);

            return "Club Deleted successfully";
        }
    }
}
