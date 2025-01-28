using T2JuniorAPI.Controllers;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Models;

namespace T2JuniorAPI.Services
{
    public interface IClubService
    {
        Task<ClubPageDTO> GetClubInfoById(string clubId);
        Task<string> CreateClub(CreateClubDTO club);
        Task<string> AddUserToClub(string clubId, AddUserToClubDTO user);
    }
}
