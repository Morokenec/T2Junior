using Microsoft.AspNetCore.Identity;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Models;
using T2JuniorAPI.Controllers;

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
            // Выполняем запрос для получения информации о клубе и его пользователях
            var clubInfo = await (from c in _context.Clubs
                                  where c.Id.ToString() == clubId // Преобразуем clubId в строку для сравнения
                                  join cu in _context.ClubUsers on c.Id equals cu.IdClub into clubGroup
                                  from cu in clubGroup.DefaultIfEmpty()
                                  join u in _context.Users on cu.IdUser equals u.Id into userGroup
                                  select new
                                  {
                                      IdClub = c.Id,
                                      ClubName = c.Name,
                                      ClubTarget = c.Target,
                                      Users = userGroup.Select(u => new SubscriberProfileDTO
                                      {
                                          Id = u.Id,
                                          FullName = $"{u.FirstName} {u.LastName}"
                                      }).ToList()
                                  }).ToListAsync(); // Изменено на ToListAsync()

            // Получаем первый элемент из списка
            var club = clubInfo.FirstOrDefault();

            if (club == null)
            {
                throw new ApplicationException("Club not found");
            }

            // Преобразуем анонимный тип в DTO
            return new ClubPageDTO
            {
                Id = club.IdClub,
                Name = club.ClubName,
                Target = club.ClubTarget,
                Users = club.Users
            };
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
    }
}
