namespace MauiApp1
{
    /// <summary>
    /// Основной класс приложения, управляющий его жизненным циклом.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Создает экземпляр приложения MAUI.
        /// </summary>
        /// <returns>Экземпляр приложения MAUI.</returns>
        public static MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        /// <summary>
        /// Конструктор класса App.
        /// </summary>
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
