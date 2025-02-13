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
        ResizeFunction();
        BindingContext = new UserViewModel();
    }

    private void OnBackButtonTapped(object sender, EventArgs e)
    {
        BackClick.OnPageClicked();
    }

    private async void OnProfileFrameTapped(object sender, EventArgs e)
    {
        var choosedFrame = sender as Frame;

        var grid = (Grid)choosedFrame.Content;

        var hsl = (HorizontalStackLayout)grid.Children[0];

        var fullNameLabel = (Label)hsl.Children[1];

        var profilePage = new ProfilePage();
        profilePage.FullName = fullNameLabel.Text;

        await Navigation.PushAsync(profilePage);
    }

    private void ResizeFunction()
    {
        
    }
}