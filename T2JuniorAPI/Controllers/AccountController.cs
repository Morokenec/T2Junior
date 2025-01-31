using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using T2JuniorAPI.Services;
using T2JuniorAPI.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, IAccountService accountService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _accountService = accountService;
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        try
        {
            var result = await _accountService.RegisterUserAsync(registerUserDto);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized("Ошибка в E-Mail или пароле");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return Unauthorized("Ошибка в E-Mail или пароле");

            var token = await _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("profile/{id}")]
    public IActionResult GetUserProfile(string id)
    {
        var userProfile = _accountService.GetUserProfileAsync(id);

        if (userProfile == null)
        {
            return NotFound(new { Message = "Профиль пользователя не найден" });
        }

        return Ok(userProfile);
    }
}
