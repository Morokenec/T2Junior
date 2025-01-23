using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            MiddleName = registerUserDto.MiddleName,
            PhoneNumber = registerUserDto.PhoneNumber,
            DateOfBirth = registerUserDto.DateOfBirth,
            Gender = registerUserDto.Gender,
            OrganizationId = registerUserDto.OrganizationId
        };

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException(string.Join("; ", result.Errors));
        }

        return "User registered successfully";
    }
}
