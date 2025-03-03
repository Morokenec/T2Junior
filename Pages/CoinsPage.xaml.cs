using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// Страница с монетами в приложении.
/// </summary>
public partial class CoinsPage : ContentPage
{
    /// <summary>
    /// Конструктор класса CoinsPage.
    /// </summary>
    public CoinsPage()
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