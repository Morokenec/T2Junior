using Microsoft.Extensions.Logging;
using T2JuniorMobile.Services;
using T2JuniorMobile.View.Pages;

namespace T2JuniorMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
   
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<AuthService>();

            // Регистрация ViewModel
            builder.Services.AddTransient<AuthViewModel>();

            // Регистрация страницы
            builder.Services.AddTransient<AuthPage>();
            builder.Services.AddTransient<RegisterPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
