using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Models;

namespace T2JuniorAPI.Services
{
    public class ClubService : IClubService
    {
        private readonly ApplicationDbContext _context;

        public ClubService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClubPageDTO> GetClubInfoById(string clubId)
        {
            var club = await _context.Clubs
                .Where(c => c.Id == clubId)
                .Select(c => new ClubPageDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Target = c.Target,
                    Users = new List<SubscriberProfileDTO>() // Инициализируем пустой список пользователей
                })
                .FirstOrDefaultAsync();

            if (club == null)
            {
                throw new ApplicationException("Club not found");
            }

            // Получаем пользователей клуба
            var users = await _context.ClubUsers
                .Where(cu => cu.IdClub == clubId)
                .Select(cu => new SubscriberProfileDTO
                {
                    Id = cu.IdUser,
                    FullName = _context.Users
                        .Where(u => u.Id == cu.IdUser)
                        .Select(u => $"{u.FirstName} {u.LastName}")
                        .FirstOrDefault()
                })
                .ToListAsync();

            // Заполняем список пользователей в клубе
            club.Users = users;

            return club;
        }

        public async Task<string> CreateClub(CreateClubDTO club)
        {
            var newClub = new Club
            {
                Name = club.Name,
                Rules = club.Rules,
                Target = club.Target,
                Raiting = 0
            };

            await _context.Clubs.AddAsync(newClub);

            var clubUser = new ClubUser
            {
                IdClub = newClub.Id,
                IdUser = club.CreatorUserId,
                IdRole = 1
            };

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

            return "Successful create club";
        }

        private bool ClubExists(string id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }

        public async Task<string> AddUserToClub(string clubId, AddUserToClubDTO user)
        {
            var club = await _context.Clubs.FindAsync(clubId);
            if (club == null)
            {
                return "Club not found";
            }

            var clubUser = new ClubUser
            {
                IdClub = clubId,
                IdUser = user.UserId,
                IdRole = user.RoleId
            };

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

        public async Task<ClubProfileDTO> GetClubProfileById(string clubId)
        {
            var club = await _context.Clubs
                .Where(c => c.Id == clubId)
                .Select(c => new ClubProfileDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Target = c.Target,
                    UsersCount = c.ClubUsers.Count()
                })
                .FirstOrDefaultAsync();
            if (club == null)
            {
                throw new ApplicationException("Club not found");
            }

            return club;
        }
    }
}
