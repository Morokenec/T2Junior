using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.ViewModels.ClubViewModel;

namespace MauiApp1;

public partial class ClubsPage : ContentPage
{
    private readonly ClubsViewModel _clubsViewModel;
    private readonly ClubProfileViewModel _clubProfileViewModel;

    // Конструктор с зависимостью IClubService
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

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void OnSearch(object sender, EventArgs e)
    {
        var clubContext = (ClubsViewModel)BindingContext;
        clubContext.FilterClubs();
    }

    private async void OnClubTapped(object sender, EventArgs e)
    {
        var tappedClub = (sender as StackLayout)?.BindingContext as Club;
        if (tappedClub != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ClubProfilePage(_clubProfileViewModel));
        }
    }
}