using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.Services.UseCase.Interface;

namespace MauiApp1;

public partial class ClubProfilePage : ContentPage
{
    private readonly ClubProfileViewModel _viewModel;

    public ClubProfilePage(IClubService clubService, Guid clubId)
    {
        InitializeComponent();
        _viewModel = new ClubProfileViewModel(clubService);
        BindingContext = _viewModel;

        // Передаем clubId в ViewModel, если это необходимо
        _viewModel.SelectedClubId = clubId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadClubProfileAsync(_viewModel.SelectedClub.ToString());
    }
}
