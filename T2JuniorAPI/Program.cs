using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using T2JuniorAPI.Data;
using T2JuniorAPI.MappingProfiles;
using T2JuniorAPI.Services.Accounts;
using T2JuniorAPI.Services.Clubs;
using T2JuniorAPI.Services.ClubRoles;
using T2JuniorAPI.Services.Events;
using T2JuniorAPI.Services.Organizations;
using T2JuniorAPI.Services.Tokens;
using T2JuniorAPI.Services.Users;
using T2JuniorAPI.Services.MediaTypes;
using T2JuniorAPI.Services.Medias;
using T2JuniorAPI.Services.MediaClubs;
using T2JuniorAPI.Services.Achievements;
using T2JuniorAPI.Services.Walls;
using T2JuniorAPI.Services.WallTypes;
using T2JuniorAPI.Services.Notes;
using T2JuniorAPI.Services.NoteStatuses;
using T2JuniorAPI.Services.MediaNotes;
using T2JuniorAPI.Services.Comments;
using T2JuniorAPI.Services.MediaComments;
using T2JuniorAPI.Services.NewsFeeds;
using T2JuniorAPI.Services.Initiatives;
using T2JuniorAPI.Repositories;
using T2JuniorAPI.Services.MediaInitiatives;



var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<SubscribersProfile>();
builder.Services.AddSingleton<SubscriptionsProfile>();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<AccountProfile>();
    config.AddProfile<AchievementProfile>();
    config.AddProfile<ClubProfile>();
    config.AddProfile<ClubRoleProfile>();
    config.AddProfile<EventProfile>();
    config.AddProfile<MediaClubProfile>();
    config.AddProfile<MediaEventProfile>();
    config.AddProfile<MediaNoteProfile>();
    config.AddProfile<MediaProfile>();
    config.AddProfile<MappingProfile>();
    config.AddProfile<MediaTypeProfile>();
    config.AddProfile<NoteProfile>();
    config.AddProfile<NoteStatusProfile>();
    config.AddProfile<OrganizationProfile>();
    config.AddProfile<SubscribersProfile>();
    config.AddProfile<SubscriptionsProfile>();
    config.AddProfile<WallProfile>();
    config.AddProfile<WallTypeProfile>();
    config.AddProfile<CommentProfile>();
    config.AddProfile<InitiativeProfile>();
});


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IClubRoleService, ClubRoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IMediaTypeService, MediaTypeService>();
builder.Services.AddScoped<IMediafileService, MediafileService>();
builder.Services.AddScoped<IMediaClubService, MediaClubService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddScoped<IWallService, WallService>();
builder.Services.AddScoped<IWallTypeService, WallTypeService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<INoteStatusService, NoteStatusService>();
builder.Services.AddScoped<IMediaNoteService, MediaNoteService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IMediaCommentService, MediaCommentService>();
builder.Services.AddScoped<INewsFeedService, NewsFeedService>();
builder.Services.AddScoped<IInitiativeService, InitiativeService>();
builder.Services.AddScoped<IMediaInitiativeService, MediaInitiativeService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var basePath = AppContext.BaseDirectory;
    c.SwaggerDoc("v1", new() { Title = "T2JuniorAPI", Version = "v1" });
    var xmlPath = Path.Combine(basePath, "documentation.xml");
    c.IncludeXmlComments(xmlPath);
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    DataInitializer.Initialize(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "T2JuniorAPI v1");
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();


