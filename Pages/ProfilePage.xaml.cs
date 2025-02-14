using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels.Profile;

namespace MauiApp1;

public partial class ProfilePage : ContentPage
{
    int netStatus = 1;
    int clickCount = 0;
    private readonly UserProfileViewModel _viewModel;

    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;
    public ProfilePage(UserProfileViewModel userProfileViewModel)
    {
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        InitializeComponent();
        ThingsByDefault();
        NetStatus();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FullNameLabel.Text = _viewModel.UserInfo?.FullName;
    }

    private void ThingsByDefault()
    {
        CoinCountLabel.Text = _viewModel.UserInfo?.SubscribersCount.ToString();
        UserRatingLabel.Text = _viewModel.UserInfo?.SubscriptionsCount.ToString();
        RatingCountLabel.Text = _viewModel.UserInfo?.ClubsCount.ToString();
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

    private async void OnProfilePhotoTapped(object sender, EventArgs e)
    {
        //try
        //{
        //    var chosenImage = await FilePicker.PickAsync(new PickOptions
        //    {
        //        PickerTitle = "Выберите изображение"
        //    });

        //    if (chosenImage != null)
        //    {
        //        var stream = await chosenImage.OpenReadAsync();
        //        AvatarImage.Source = ImageSource.FromStream(() => stream);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    await DisplayAlert("Фото не было выбрано.", ex.Message, "OK");
        //}
    }

    private async void OnCoinButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

    private async void OnRatingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RatingPage());
    }

    private async void OnSubscribersButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SubscribersPage());
    }

    private async void OnFollowingButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FollowingPage());
    }

    private async void OnClubsButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClubsPage());
    }

    private async void OnProjectsFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectsPage());
    }

    private async void OnActivitiesFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActivitiesPage());
    }

    private async void OnCalendarFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalendarPage());
    }

    private async void OnNewsFrameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

    private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    {
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
    }
}