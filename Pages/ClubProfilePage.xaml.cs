using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.Services.UseCase.Interface;

namespace MauiApp1;

/// <summary>
/// Страница профиля клуба в приложении.
/// </summary>
public partial class ClubProfilePage : ContentPage
{
    private readonly ClubProfileViewModel _viewModel;

    /// <summary>
    /// Конструктор класса ClubProfilePage.
    /// </summary>
    /// <param name="clubProfileViewModel">Модель представления для управления профилем клуба.</param>
    public ClubProfilePage(ClubProfileViewModel clubProfileViewModel)
    {
        InitializeComponent();
        _viewModel = clubProfileViewModel;
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Метод, вызываемый при отображении страницы.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadClubsAsync();
    }
}
