using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using T2Junior.Data;
using T2Junior.Models;
using T2Junior.Options;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _context.Users
            .Include(u => u.IdRoleNavigation) // Включаем роль пользователя
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        // Проверяем, существует ли пользователь и совпадает ли пароль
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            return Unauthorized(new { message = "Неверный логин или пароль" });
        }

        // Создаем claims для токена
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(200)), // Время жизни токена
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Проверяем, существует ли пользователь с таким email
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (existingUser != null)
        {
            return Conflict(new { message = "Пользователь с таким email уже существует" });
        }

        // Хэшируем пароль
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // Создаем нового пользователя
        var user = new User
        {
            IdRole = 1, // Например, роль "Пользователь" (IdRole = 2)
            FirstName = model.FirstName,
            LastName = model.LastName,
            Patronymic = model.Patronymic,
            Organization = model.Organization,
            Post = model.Post,
            Age = model.Age,
            Sex = model.Sex,
            Email = model.Email,
            Phone = model.Phone,
            Password = passwordHash,
            AccumulatedPoints = 0 // Начальное количество накопленных очков
        };

        // Сохраняем пользователя в базу данных
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Пользователь успешно зарегистрирован" });
    }

    public class RegisterModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string? Organization { get; set; }
        public string? Post { get; set; }
        public int? Age { get; set; }
        public sbyte Sex { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}