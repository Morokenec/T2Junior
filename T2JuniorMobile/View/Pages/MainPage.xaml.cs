using T2JuniorMobile.Services;

namespace T2JuniorMobile
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            HttpClient _httpClient = new HttpClient();
            AuthService _authService = new AuthService(_httpClient);
            BindingContext = new AuthViewModel(_authService);
            InitializeComponent();
        }
    }

}
