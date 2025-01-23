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
            ValidateIssuer = true, // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidIssuer = AuthOptions.ISSUER, // ������, �������������� ��������
            ValidateAudience = true, // ����� �� �������������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE, // ��������� ����������� ������
            ValidateLifetime = true, // ����� �� �������������� ����� �������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), // ��������� ����� ������������
            ValidateIssuerSigningKey = true, // ��������� ����� ������������
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
