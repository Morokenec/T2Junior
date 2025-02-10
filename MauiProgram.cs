using Microsoft.Extensions.Logging;

namespace MauiApp1
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
                    fonts.AddFont("Manrope-ExtraBold.ttf", "ManropeExtraBold");
                    fonts.AddFont("Manrope-ExtraLight.ttf", "ManropeExtraLight");
                    fonts.AddFont("RedRose-Bold.ttf", "RedRoseBold");
                    fonts.AddFont("MyriadPro-Bold.otf", "MyriadProBold");
                    fonts.AddFont("Manrope-Medium.ttf", "ManropeMid");
                    fonts.AddFont("MyriadPro-Regular.otf", "MyriadPro");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
