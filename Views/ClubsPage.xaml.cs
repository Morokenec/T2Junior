using MauiApp1.DataModel;
using MauiApp1.Services;
using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class ClubsPage : ContentPage
{
    public bool Redirected { get; set; } = BackNavigationState.IsDirectAccess;
    public ClubsPage()
    {
        InitializeComponent();
        BindingContext = new ClubViewModel();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private void OnSearch(object sender, EventArgs e)
    {
        var clubContext = (ClubViewModel)BindingContext;
        clubContext.FilterClubs();
    }

    private void OnClubTapped(object sender, EventArgs e)
    {
        var tappedClub = (sender as Frame)?.BindingContext as Club;

        if (tappedClub != null)
        {
            var clubProfilePage = new ClubProfilePage
            {
                SelectedClubId = tappedClub.IdClub
            };
            Application.Current.MainPage.Navigation.PushAsync(clubProfilePage);
        }
    }
}