using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class ProfilePage : ContentPage
{
    int netStatus = 1;
    int userRating = 5;
    int ratingCount = 105;
    int clickCount = 0;

    public string FullName { get; set; }
    public int CoinCount { get; set; }
    public int SelectedProfileId { get; set; } = 0;
    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;
    public ProfilePage()
    {
        InitializeComponent();
        NetStatus();
        BindingContext = new UserProfileViewModel();
        UserRatingLabel.Text = userRating.ToString();
        RatingCountLabel.Text = ratingCount.ToString();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfileDetails(SelectedProfileId);
        FullNameLabel.Text = FullName;
        CoinCountLabel.Text = CoinCount.ToString();
    }

    private void NetStatus()
    {
        if (netStatus == 0)
        {
            CircleOverlay.BackgroundColor = Colors.Grey;
        }
        else
        {
            CircleOverlay.BackgroundColor = Color.FromArgb("#3DC47C");
        }
    }

    private void OnCoinButtonTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CoinsPage());
    }

    private void OnRatingButtonTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RatingPage());
    }

    private void OnSubscribersButtonTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SubscribersPage());
    }

    private void OnFollowingButtonTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FollowingPage());
    }

    private void OnClubsButtonTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ClubsPage());
    }

    private void OnProjectsFrameTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProjectsPage());
    }

    private void OnActivitiesFrameTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ActivitiesPage());
    }

    private void OnCalendarFrameTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CalendarPage());
    }

    private void OnNewsFrameTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NotesPage());
    }

    private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    {
        clickCount++;

        if (clickCount == 1)
        {
            SubscribeButton.BackgroundColor = Colors.White;
            SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
            SubscribeButton.Text = "Вы подписаны";
            SubscribeButton.TextColor = Color.FromArgb("#0057A6");
        }
        else if (clickCount == 2)
        {
            SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
            SubscribeButton.BorderColor = Colors.White;
            SubscribeButton.Text = "Подписаться";
            SubscribeButton.TextColor = Colors.White;
            clickCount = 0;
        }
    }

    private void LoadProfileDetails(int? selectedProfileId)
    {
        var userViewModel = new UserViewModel();
        var profileViewModel = (UserProfileViewModel)BindingContext;

        UserProfileDTO selectedProfile;

        if (selectedProfileId.Value > 0)
        {
            selectedProfile = userViewModel.GetProfileById(selectedProfileId.Value);
            SelectedProfileId = 0;
        }
        else
        {
            selectedProfile = userViewModel.UserProfiles.FirstOrDefault();
        }

        profileViewModel.SelectedProfile = selectedProfile;
        FullName = selectedProfile.Name;
        CoinCount = selectedProfile.AccumulatedPoints;
    }

    public void SetSelectedUserId(int selectedClubId)
    {
        SelectedProfileId = selectedClubId;
        LoadProfileDetails(SelectedProfileId);
    }
}