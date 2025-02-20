using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using T2JuniorAPI.Services.Tokens;

/// <summary>
/// Сервис для работы с токенами
/// </summary>
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор для внедрения зависимостей
    /// </summary>
    /// <param name="configuration">Конфигурация приложения</param>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Генерирует токен для указанного пользователя
    /// </summary>
    /// <param name="user">Пользователь, для которого генерируется токен</param>
    /// <returns>Строка-токен</returns>
    public Task<string> GenerateToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: creds
        );

        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = new ClaimsIdentity(claims),
        //    Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
        //    Issuer = _configuration["Jwt:Issuer"],
        //    Audience = _configuration["Jwt:Audience"],
        //    SigningCredentials = creds
        //};

        //var tokenHandler = new JwtSecurityTokenHandler();
        //var token1 = tokenHandler.CreateToken(tokenDescriptor); 

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        //return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token1));
    }
}
