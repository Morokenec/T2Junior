using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using T2WebSoket;
using T2WebSoket.Hubs;
using T2WebSoket.MappingProfiles;
using T2WebSoket.Models;
using T2WebSoket.Repositories;
using T2WebSoket.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });
    //var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ChatConnection")));

builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ChatDbContext>();

    if (!dbContext.ChatTypes.Any())
    {
        var chatTypes = new List<ChatType>
        {
            new ChatType {Name = "Private"},
            new ChatType {Name = "Group"}
        };

        dbContext.ChatTypes.AddRange(chatTypes);
        dbContext.SaveChanges();
    }
}
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API v1");
    c.RoutePrefix = "swagger"; // Установите пустой префикс маршрута для доступа по корневому URL
});


app.MapHub<ChatHub>("/chatHub");
app.MapControllers();
app.MapFallbackToFile("index.html");

//app.MapControllers();
app.Run();