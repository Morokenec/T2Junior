using CommunityToolkit.Maui;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
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

                builder.Services.AddSingleton<IJsonDeserializerService, JsonDeserializerService>();
                builder.Services.AddSingleton<ITaiyoService, TaiyoService>();
                builder.Services.AddSingleton<UserProfileViewModel>();
                builder.Services.AddTransient<ProfilePage>();

                builder.Services.AddHttpClient<ITaiyoService, TaiyoService>();

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
