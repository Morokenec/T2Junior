namespace MauiApp1.Services.AppHelper
{
    /// <summary>
    /// Класс для обработки события нажатия кнопки "Назад".
    /// </summary>
    public static class BackClick
    {
        /// <summary>
        /// Событие, возникающее при нажатии кнопки "Назад".
        /// </summary>
        public static event EventHandler PageClicked;

        /// <summary>
        /// Конструктор класса BackClick.
        /// </summary>
        static BackClick()
        {
            PageClicked += HandleOfPageClicked;
        }

        /// <summary>
        /// Метод для вызова события нажатия кнопки "Назад".
        /// </summary>
        public static void OnPageClicked()
        {
            PageClicked?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Назад".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        public async static void HandleOfPageClicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                if (navigationPage.Navigation.NavigationStack.Count > 1)
                {
                    await navigationPage.PopAsync();
                }
            }
            else if (Application.Current.MainPage is Shell shell)
            {
                await shell.GoToAsync("..");
            }
        }
    }
}
