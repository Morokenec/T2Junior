using MauiApp1.Services.AppHelper;

namespace MauiApp1;

/// <summary>
/// Страница с правилами приложения.
/// </summary>
public partial class RulesPage : ContentPage
{
    /// <summary>
    /// Конструктор класса RulesPage.
    /// </summary>
    public RulesPage()
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