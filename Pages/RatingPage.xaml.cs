using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// Страница с рейтингами в приложении.
/// </summary>
public partial class RatingPage : ContentPage
{
    /// <summary>
    /// Конструктор класса RatingPage.
    /// </summary>
    public RatingPage()
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