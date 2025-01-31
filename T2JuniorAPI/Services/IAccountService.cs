using System.Threading.Tasks;
using T2JuniorAPI.DTOs;

public interface IAccountService
{
    Task<UserProfileDTO> GetUserProfileAsync(string userId);
    Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
}
