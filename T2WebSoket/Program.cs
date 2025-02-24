using Microsoft.EntityFrameworkCore;
using T2WebSoket;
using T2WebSoket.Hubs;
using T2WebSoket.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ChatConnection")));

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.Run();