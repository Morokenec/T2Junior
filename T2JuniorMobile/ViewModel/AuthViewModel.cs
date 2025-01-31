using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using T2JuniorMobile.Services;
using T2JuniorMobile.View.Pages;

public class AuthViewModel : BaseViewModel
{
    public ICommand LoginCommand { get; }
    public ICommand NavigateRegisterCommand { get; }

    private string _email;
    private string _password;
    private string _token;
    private readonly AccountService _authService;

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

    public AuthViewModel(AccountService authService)
    {
        _authService = authService;
        LoginCommand = new Command(async () => await LoginAsync());
        NavigateRegisterCommand = new Command(async () => await NavigateToRegisterPageAsync());
    }

    private async Task LoginAsync()
    {
        try
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
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Сервер недоступен", "OK");
        }
    }

    private async Task NavigateToRegisterPageAsync()
    {
        await Shell.Current.GoToAsync("/RegisterPage");
    }
}