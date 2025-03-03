using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// Начальная страница приложения.
/// </summary>
public partial class InitiumPage : ContentPage
{
    /// <summary>
    /// Конструктор класса InitiumPage.
    /// </summary>
    public InitiumPage()
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