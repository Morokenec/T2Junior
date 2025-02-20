using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.ClubViewModel;

namespace MauiApp1;

public partial class ClubsPage : ContentPage
{
    private readonly IClubService _clubService;

    // ����������� � ������������ IClubService
    public ClubsPage(IClubService clubService)
    {
        InitializeComponent();
        _clubService = clubService ?? throw new ArgumentNullException(nameof(clubService));
        BindingContext = new ClubsViewModel();

        // ��������� ����� ��� �������������
        LoadClubsAsync();
    }

    private async Task LoadClubsAsync()
    {
        var clubsViewModel = (ClubsViewModel)BindingContext;
        clubsViewModel.LoadClubsAsync();
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
            // ��������� � ��������� ID �����
            await Application.Current.MainPage.Navigation.PushAsync(new ClubProfilePage(_clubService, tappedClub.id));
        }
    }
}