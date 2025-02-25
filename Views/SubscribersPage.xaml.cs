using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class SubscribersPage : ContentPage
{
    public SubscribersPage()
    {
        InitializeComponent();
        BindingContext = new UserViewModel();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private async void OnProfileFrameTapped(object sender, EventArgs e)
    {
        var choosedFrame = sender as Frame;

        var profileId = choosedFrame.BindingContext as UserProfileDTO;

        ProfilePage.SelectedProfileId = profileId.IdUser;

        await Navigation.PushAsync(new ProfilePage());
    }
}