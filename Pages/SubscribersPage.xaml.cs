using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Profile;
using MauiApp1.ViewModels.ProfileModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class SubscribersPage : ContentPage
{
    private readonly SubscribersViewModel _viewModel;

    public SubscribersPage(SubscribersViewModel subscribersViewModel)
    {
        _viewModel = subscribersViewModel;
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
        await _viewModel.LoadUsers();
    }
    private async void OnProfileFrameTapped(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(ProfilePage))
            return;
    }
}