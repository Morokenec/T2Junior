using MauiApp1.Services.AppHelper;
using MauiApp1.ViewModels.Profile;
using MauiApp1.ViewModels.ProfileModels;

namespace MauiApp1;

/// <summary>
/// Страница подписок пользователя в приложении.
/// </summary>
public partial class FollowingPage : ContentPage
{
    private FollowingViewModel _viewModel;
    public FollowingPage(FollowingViewModel followingViewModel)
    {
        _viewModel = followingViewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUsers();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }
}