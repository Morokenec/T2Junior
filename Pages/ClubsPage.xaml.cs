using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
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

    public ClubsPage(ClubsViewModel clubsViewModel)
    {
        InitializeComponent();
        _clubsViewModel = clubsViewModel;
        BindingContext = _clubsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _clubsViewModel.LoadClubsAsync();
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
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ClubProfilePage))
            return;

        var tappedClub = (sender as StackLayout)?.BindingContext as Club;
        if (tappedClub != null)
        {
            HttpClient httpClient = new HttpClient();
            JsonDeserializerService jsonDeserializerService = new JsonDeserializerService();
            await Application.Current.MainPage.Navigation.PushAsync(new ClubProfilePage(new ClubProfileViewModel(new ClubService(httpClient, jsonDeserializerService), new NoteService(httpClient, jsonDeserializerService), tappedClub.Id)));
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ClubProfilePage))
            return;

        var tappedClub = (sender as Frame)?.BindingContext as Club;
        if (tappedClub != null)
        {
            HttpClient httpClient = new HttpClient();
            JsonDeserializerService jsonDeserializerService = new JsonDeserializerService();
            await Application.Current.MainPage.Navigation.PushAsync(
                new ClubProfilePage(new ClubProfileViewModel(
                    new ClubService(httpClient, jsonDeserializerService),
                    new NoteService(httpClient, jsonDeserializerService),
                    tappedClub.Id)));
        }
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}