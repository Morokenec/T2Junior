using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.Services.UseCase.Interface;

namespace MauiApp1;

public partial class ClubProfilePage : ContentPage
{
    private readonly ClubProfileViewModel _viewModel;

    public ClubProfilePage(ClubProfileViewModel clubProfileViewModel)
    {
        InitializeComponent();
        _viewModel = clubProfileViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ClubProfileViewModel viewModel)
        {
            await viewModel.LoadClubProfileAsync();

        }
    }

    private void SubscribeButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is ClubProfileViewModel viewModel)
        {
            viewModel.SubscribeClub();
        }
    }
}
