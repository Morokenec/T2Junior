using MauiApp1.Models;
using MauiApp1.Models.ProfileModels;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Profile;
using MauiApp1.ViewModels.ProfileModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

/// <summary>
/// Страница подписчиков, отображающая список подписчиков.
/// </summary>
public partial class SubscribersPage : ContentPage
{
    private readonly SubscribersViewModel _viewModel;

    HttpClient _httpClient = new HttpClient();
    JsonDeserializerService _jsonDeserializerService = new JsonDeserializerService();
    private Guid _userId;


    public SubscribersPage(SubscribersViewModel subscribersViewModel, Guid userId)
    {
        _viewModel = subscribersViewModel;
        _userId = userId;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    private void OnBackButtonTapped(object sender, EventArgs e) 
    {
        BackClick.OnPageClicked();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUsers(_userId);
    }

    private async void OnProfileFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ProfilePage))
            return;

        var tappedItem = (sender as Frame)?.BindingContext as UserSocial;
        if (tappedItem != null)
        {
            Guid userId = Guid.Parse(tappedItem.Id); // Получаем ID нажатого объекта
            await Navigation.PushAsync(new ProfilePage(new UserProfileViewModel(new ProfileService(_httpClient, _jsonDeserializerService),
                new NoteService(_httpClient, _jsonDeserializerService)), userId), true);
        }
    }
}