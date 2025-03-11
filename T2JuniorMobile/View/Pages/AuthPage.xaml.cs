using T2JuniorMobile.Services;

namespace T2JuniorMobile.View.Pages
{
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            HttpClient _httpClient = new HttpClient();
            AccountService _accountService = new AccountService(_httpClient);
            BindingContext = new AuthViewModel(_accountService);
            InitializeComponent();
        }
    }

}
