using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using T2JuniorMobile.Services;
using T2JuniorMobile.View.Pages;
using T2JuniorMobile.ViewModel;

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
            builder.Services.AddSingleton<AccountService>();

            // Регистрация ViewModel
            builder.Services.AddTransient<AuthViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ConfirmViewModel>();

            // Регистрация страницы
            builder.Services.AddTransient<ConfimPage>();
            builder.Services.AddTransient<AuthPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ClubsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
