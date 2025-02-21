using CommunityToolkit.Maui;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.ViewModels.ClubViewModel;
using MauiApp1.ViewModels.Profile;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            try
            {
                var builder = MauiApp.CreateBuilder();

                builder
                    .UseMauiApp<App>()
                    .UseMauiCommunityToolkit()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("Manrope-ExtraBold.ttf", "ManropeExtraBold");
                        fonts.AddFont("Manrope-ExtraLight.ttf", "ManropeExtraLight");
                        fonts.AddFont("RedRose-Bold.ttf", "RedRoseBold");
                        fonts.AddFont("MyriadPro-Bold.otf", "MyriadProBold");
                        fonts.AddFont("Manrope-Medium.ttf", "ManropeMid");
                        fonts.AddFont("MyriadPro-Regular.otf", "MyriadPro");
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

                builder.Services.AddTransient<IJsonDeserializerService, JsonDeserializerService>();
                builder.Services.AddSingleton<IProfileService, ProfileService>();
                builder.Services.AddSingleton<IClubService, ClubService>();
                builder.Services.AddSingleton<INoteService, NoteService>();

                builder.Services.AddSingleton<UserProfileViewModel>();
                builder.Services.AddSingleton<ClubsViewModel>();
                builder.Services.AddSingleton<ClubProfileViewModel>();

                builder.Services.AddTransient<ProfilePage>();
                builder.Services.AddTransient<ClubsPage>();
                builder.Services.AddTransient<ClubProfilePage>();

                builder.Services.AddSingleton<HttpClient>();

                builder.Services.AddHttpClient<IProfileService, ProfileService>();

#if DEBUG
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] MAUI initialization failed: {ex.Message}");
                throw;
            }
        }
    }
}
