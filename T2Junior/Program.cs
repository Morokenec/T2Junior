using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using T2Junior.Data;
using T2Junior.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // указывает, будет ли валидироваться издатель при валидации токена
            ValidIssuer = AuthOptions.ISSUER, // строка, представляющая издателя
            ValidateAudience = true, // будет ли валидироваться потребитель токена
            ValidAudience = AuthOptions.AUDIENCE, // установка потребителя токена
            ValidateLifetime = true, // будет ли валидироваться время существования
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
            ValidateIssuerSigningKey = true, // валидация ключа безопасности
        };
    });

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
