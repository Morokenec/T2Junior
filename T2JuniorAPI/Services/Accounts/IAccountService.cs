using System.Threading.Tasks;
using T2JuniorAPI.DTOs.Users;

public interface IAccountService
{
    Task<UserProfileDTO> GetUserProfileAsync(Guid userId);
    Task<List<UserProfileDTO>> GetAllUserProfilesAsync();
    Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<string> UpdateUserProfileAsync(Guid userId, UpdateUserDto updateUserDto);
    Task<string> DeleteUserAsync(Guid userId);
    Task<string> UserPasswordRecovery(RecoveryPasswordDTO recoveryPassword);
}
