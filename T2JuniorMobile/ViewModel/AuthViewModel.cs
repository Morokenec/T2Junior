using System.IdentityModel.Tokens.Jwt;
using System.Windows.Input;
using T2JuniorMobile.Services;

//Не знаю может смысла нет разжевывать следующее, но лучше будет, чем не будет.
/// <summary>
/// ViewModel для управления аутентификацией пользователя.
/// </summary>
public class AuthViewModel : BaseViewModel
{
    public ICommand LoginCommand { get; }
    public ICommand NavigateRegisterCommand { get; }

    private string _email;
    private string _password;
    private string _token;
    private readonly AccountService _authService;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    /// <summary>
    /// Токен аутентификации.
    /// </summary>
    public string Token
    {
        get => _token;
        set => SetProperty(ref _token, value);
    }

    /// <summary>
    /// Конструктор класса AuthViewModel.
    /// </summary>
    /// <param name="authService">Сервис для аутентификации пользователей.</param>
    public AuthViewModel(AccountService authService)
    {
        _authService = authService;
        LoginCommand = new Command(async () => await LoginAsync());
        NavigateRegisterCommand = new Command(async () => await NavigateToRegisterPageAsync());
    }

    /// <summary>
    /// Выполнение входа пользователя в систему.
    /// </summary>
    private async Task LoginAsync()
    {
        try
        {
            Token = await _authService.LoginAsync(Email, Password);

            if (string.IsNullOrEmpty(Token))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Неверный email или пароль", "OK");
                return;
            }

            await SecureStorage.Default.SetAsync("jwt_token", Token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(Token);
            var uid = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (!string.IsNullOrEmpty(uid))
            {
                try
                {   
                    await SecureStorage.Default.SetAsync("user_uid", uid);
                    await Shell.Current.GoToAsync("/ProfilePage");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ошибка", $"Не удалось сохранить UID {ex.Data}", "OK");
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Сервер недоступен", "OK");
        }
    }

    /// <summary>
    /// Переход на страницу регистрации.
    /// </summary>
    private async Task NavigateToRegisterPageAsync()
    {
        await Shell.Current.GoToAsync("/RegisterPage");
    }
}