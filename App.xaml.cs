namespace MauiApp1
{
    public partial class App : Application
    {
        public static MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
