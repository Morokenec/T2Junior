using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using T2JuniorMobile.Services;

public class AuthViewModel : BaseViewModel
{
    private string _email;
    private string _password;
    private string _token;
    private readonly AuthService _authService;

    public AuthViewModel(AuthService authService)
    {
        _authService = authService;
        LoginCommand = new Command(async () => await LoginAsync());
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string Token
    {
        get => _token;
        set => SetProperty(ref _token, value);
    }

    public ICommand LoginCommand { get; }

    private async Task LoginAsync()
    {
        Token = await _authService.LoginAsync(Email, Password);

        if (string.IsNullOrEmpty(Token))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Неверный email или пароль", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Успех", "Авторизация прошла успешно", "OK");
            await SecureStorage.SetAsync("jwt_token", Token);
        }
    }
}