using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Services.Tokens;

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

    /// <summary>
    /// Регистрация аккаунта.
    /// </summary>
    /// <param name="registerUserDto">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
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

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="loginDto">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
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

    /// <summary>
    /// Получение профиля по ID.
    /// </summary>
    /// <param name="id">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpGet("profile/{id}")]
    public IActionResult GetUserProfile(Guid id)
    {
        var userProfile = _accountService.GetUserProfileAsync(id);

        if (userProfile == null)
        {
            return NotFound(new { Message = "Профиль пользователя не найден" });
        }

        return Ok(userProfile);
    }

    /// <summary>
    /// Получение всех профилей.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpGet("all")]
    public IActionResult GetAllUsers()
    {
        var userProfile = _accountService.GetAllUserProfilesAsync();

        if (userProfile == null)
        {
            return NotFound(new { Message = "Профиль пользователя не найден" });
        }

        return Ok(userProfile);
    }

    /// <summary>
    /// Обновление профиля пользователя.
    /// </summary>
    /// <param name="id">Пользователь</param>
    /// <param name="updateUserDto">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpPut("change/{id}")]
    public async Task<IActionResult> UpdateUserProfileById(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            var update_user = await _accountService.UpdateUserProfileAsync(id, updateUserDto);
            return Ok(update_user);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    /// <summary>
    /// Восстановление пароля пользователя.
    /// </summary>
    /// <param name="recoveryPassword">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpPut("password-recovery")]
    public async Task<IActionResult> RecoveryUserPassword([FromBody] RecoveryPasswordDTO recoveryPassword)
    {
        try
        {
            var recovery = await _accountService.UserPasswordRecovery(recoveryPassword);
            return Ok(recovery);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    /// <summary>
    /// Удаление пользователя по ID.
    /// </summary>
    /// <param name="id">Пользователь</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _accountService.DeleteUserAsync(id);
            return Ok(new { Message = "User successfully deleted" });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
