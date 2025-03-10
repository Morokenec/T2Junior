using MauiApp1.DataModel;
using MauiApp1.Pages;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.ClubViewModel;
using MauiApp1.ViewModels.Profile;
using MauiApp1.ViewModels.ProfileModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1;

public partial class ProfilePage : ContentPage
{
    int netStatus = 1;

    private readonly UserProfileViewModel _viewModel;

    private Guid _userId;

    public bool DirectAccessed { get; set; } = BackNavigationState.IsDirectAccess;

    public ProfilePage(UserProfileViewModel userProfileViewModel)
    {
        InitializeComponent();
        _userId = Guid.Parse(AppSettings.test_user_guid);
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        NetStatus();
    }

    public ProfilePage(UserProfileViewModel userProfileViewModel, Guid userId)
    {
        InitializeComponent();
        _userId = userId;
        _viewModel = userProfileViewModel;
        BindingContext = _viewModel;
        NetStatus();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is UserProfileViewModel viewModel)
        {
            await viewModel.LoadDataAsync(_userId);
         
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
        if (BindingContext is UserProfileViewModel viewModel)
        {
            await viewModel.SetAvatarProfile();
        }
    }

    private async void OnCoinButtonTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(NotesPage))
            return;

        await Navigation.PushAsync(new NotesPage(new NoteViewModel(new NoteService(new HttpClient(), new JsonDeserializerService()))), true);
    }

    private async void OnRatingButtonTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(RatingPage))
            return;

         await Navigation.PushAsync(new RatingPage());
    }

    private async void OnSubscribersButtonTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(SubscribersPage))
            return;

        await Navigation.PushAsync(new SubscribersPage(new SubscribersViewModel(new ProfileService(new HttpClient(), new JsonDeserializerService())), Guid.Parse(_viewModel.UserInfo.Id)));
    }

    private async void OnFollowingButtonTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(FollowingPage))
            return;

        await Navigation.PushAsync(new FollowingPage(new FollowingViewModel(new ProfileService(new HttpClient(), new JsonDeserializerService()))));
    }

    private async void OnClubsButtonTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ClubsPage))
            return;

        await Navigation.PushAsync(new ClubsPage(new ClubsViewModel(true)));
    }

    private  async void OnProjectsFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ProjectsPage))
            return;

        await Navigation.PushAsync(new ProjectsPage());
    }

    private async void OnActivitiesFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ActivitiesPage))
            return;

        await Navigation.PushAsync(new ActivitiesPage());
    }

    private async void OnCalendarFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(CalendarPage))
            return;

        await Navigation.PushAsync(new CalendarPage());
    }

    private async void OnNewsFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(NotesPage))
            return;

        await Navigation.PushAsync(new NotesPage(new NoteViewModel(new NoteService(new HttpClient(), new JsonDeserializerService()))), true);
    }

    private async void NoteCreateButton_Clicked(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(NoteEditorPage))
            return;

        await Navigation.PushAsync(new NoteEditorPage(new NoteEditorViewModel(new NoteService(new HttpClient(), new JsonDeserializerService()), Guid.Parse(AppSettings.test_user_guid))));
    }

    //private void OnSubUnsubButtonTapped(object sender, EventArgs e)
    //{
    //    {
    //        clickCount++;

    //        if (clickCount == 1)
    //        {
    //            SubscribeButton.BackgroundColor = Colors.White;
    //            SubscribeButton.BorderColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.Text = "�� ���������";
    //            SubscribeButton.TextColor = Color.FromArgb("#0057A6");
    //        }
    //        else if (clickCount == 2)
    //        {
    //            SubscribeButton.BackgroundColor = Color.FromArgb("#0057A6");
    //            SubscribeButton.BorderColor = Colors.White;
    //            SubscribeButton.Text = "�����������";
    //            SubscribeButton.TextColor = Colors.White;
    //            clickCount = 0;
    //        }
    //    }
    //}   
}