using T2JuniorAPI.DTOs.Clubs;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Clubs
{
    public interface IClubService
    {
        Task<ClubPageDTO> GetClubInfoById(Guid clubId);
        Task<string> CreateClub(CreateClubDTO club);
        Task<string> AddUserToClub(Guid clubId, AddUserToClubDTO user);
        Task<string> DeleteUserFromClub(Guid clubId, Guid userId);
        Task<ClubProfileDTO> GetClubProfileById(Guid clubId);
        Task<List<AllClubsDTO>> GetAllClubsByUserId(Guid userId);
        Task<string> UpdateClub(Guid id, UpdateClubDTO updateClubDto);
        Task<string> DeleteClub(Guid id);
        Task<List<AllClubsDTO>> GetAllClubs();

    }
}
