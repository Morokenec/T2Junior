using MauiApp1.ViewModels.ClubProfileViewModel;
using MauiApp1.Services.UseCase.Interface;
using MauiApp1.Pages;
using MauiApp1.ViewModels;
using MauiApp1.Services.UseCase;
using MauiApp1.Services.AppHelper;

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
            viewModel.LoadClubProfileAsync();
        }
    }

    private async void SendNewsButton_Clicked(object sender, EventArgs e)
    {
        var currentPage = Navigation.NavigationStack.LastOrDefault();
        if (currentPage?.GetType() == typeof(NoteEditorPage))
            return;

        await Navigation.PushAsync(new NoteEditorPage(new NoteEditorViewModel(new NoteService(new HttpClient(), new JsonDeserializerService()), _viewModel.SelectedClub.Id)));
    }

    private async void ClubAvatar_Tapped(object sender, TappedEventArgs e)
    {
        if (BindingContext is ClubProfileViewModel viewModel)
        {
            await viewModel.SetAvatarClub();
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {

    }
}
