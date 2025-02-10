namespace T2JuniorMobile.View.Pages;

using T2JuniorMobile.Services;
using T2JuniorMobile.ViewModel;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _viewModel;
    private readonly ProfileServices _profileServices;
    private readonly HttpClient _httpClient = new HttpClient();

    public ProfilePage()
    {
        InitializeComponent();
        _profileServices = new ProfileServices(_httpClient);
        _viewModel = new ProfileViewModel(_profileServices);
        BindingContext = _viewModel;
    }
}