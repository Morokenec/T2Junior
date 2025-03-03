using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.ViewModels.ClubViewModel;

namespace MauiApp1;

/// <summary>
/// Страница с клубами в приложении.
/// </summary>
public partial class ClubsPage : ContentPage
{
    private readonly ClubsViewModel _clubsViewModel;
    private readonly ClubProfileViewModel _clubProfileViewModel;

    /// <summary>
    /// Конструктор класса ClubsPage с зависимостью ClubsViewModel.
    /// </summary>
    /// <param name="clubsViewModel">Модель представления для управления клубами.</param>
    public ClubsPage(ClubsViewModel clubsViewModel)
    {
        InitializeComponent();
        _clubsViewModel = clubsViewModel;
        BindingContext = _clubsViewModel;

        clubsViewModel.LoadClubsAsync();
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

    /// <summary>
    /// Обработчик события поиска.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void OnSearch(object sender, EventArgs e)
    {
        var clubContext = (ClubsViewModel)BindingContext;
        clubContext.FilterClubs();
    }

    /// <summary>
    /// Обработчик события нажатия на клуб.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private async void OnClubTapped(object sender, EventArgs e)
    {
        var tappedClub = (sender as StackLayout)?.BindingContext as Club;
        if (tappedClub != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ClubProfilePage(_clubProfileViewModel));
        }
    }
}