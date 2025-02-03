using T2JuniorAPI.DTOs;

namespace T2JuniorAPI.Services
{
    public interface IClubRoleService
    {
        Task<List<ClubRolesDTO>> GetAllClubRolesAsync();
        Task<ClubRolesDTO> GetClubRoleByIdAsync(Guid id);
        Task<ClubRolesDTO> CreateClubRoleAsync(ClubRolesDTO clubRoleDto);
        Task<ClubRolesDTO> UpdateClubRoleAsync(Guid id, ClubRolesDTO clubRoleDto);
        Task<bool> DeleteClubRoleAsync(Guid id);
    }
}
