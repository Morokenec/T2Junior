using MauiApp1.DataModel;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels.Profile;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ProfilePage : ContentPage
{
    int netStatus = 1;
    //int clickCount = 0;
    private readonly UserProfileViewModel _viewModel;

    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;
    public ProfilePage(UserProfileViewModel userProfileViewModel)
    {
        InitializeComponent();
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        NetStatus();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Вызов команды при появлении страницы
        if (BindingContext is UserProfileViewModel viewModel)
        {
            await viewModel.LoadDataAsync();
         
        }
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
        try
        {
            var chosenImage = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите изображение"
            });

            if (chosenImage != null)
            {
                var stream = await chosenImage.OpenReadAsync();
                //AvatarImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Фото не было выбрано.", ex.Message, "OK");
        }
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
        IClubService _clubService = new ClubService(new HttpClient(), new JsonDeserializerService());
        await Navigation.PushAsync(new ClubsPage(_clubService));
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

    //private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    //{
    //    {
    //        clickCount++;

    //        if (clickCount == 1)
    //        {
    //            SubscribeButton.BackgroundColor = Colors.White;
    //            SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.Text = "Вы подписаны";
    //            SubscribeButton.TextColor = Color.FromArgb("#0057A6");
    //        }
    //        else if (clickCount == 2)
    //        {
    //            SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.BorderColor = Colors.White;
    //            SubscribeButton.Text = "Подписаться";
    //            SubscribeButton.TextColor = Colors.White;
    //            clickCount = 0;
    //        }
    //    }
    //}
}