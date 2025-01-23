using T2JuniorMobile.Services;

namespace T2JuniorMobile.View.Pages
{
    public partial class AuthPage : ContentPage
    {

        public AuthPage()
        {
            HttpClient _httpClient = new HttpClient();
            AuthService _authService = new AuthService(_httpClient);
            BindingContext = new AuthViewModel(_authService);
            InitializeComponent();
        }
    }

}
