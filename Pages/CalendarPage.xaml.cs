using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// Страница календаря в приложении.
/// </summary>
public partial class CalendarPage : ContentPage
{
    /// <summary>
    /// Конструктор класса CalendarPage.
    /// </summary>
    public CalendarPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку "Назад".
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}