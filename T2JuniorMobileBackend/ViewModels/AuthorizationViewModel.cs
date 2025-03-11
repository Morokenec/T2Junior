using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    /// <summary>
    /// ViewModel для авторизации пользователя.
    /// Обрабатывает ввод данных, взаимодействует с сервисом профиля и управляет состоянием авторизации.
    /// </summary>
    public class AuthorizationViewModel : BindableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Команда для выполнения входа в систему.
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Событие, вызываемое при закрытии модального окна.
        /// </summary>
        public event EventHandler ModalClosed;

        private string _email;
        private string _password;
        private string _token;

        /// <summary>
        /// Email пользователя для авторизации.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Пароль пользователя для авторизации.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// JWT-токен, полученный после успешного входа.
        /// </summary>
        public string Token
        {
            get => _token;
            set
            {
                if (_token != value)
                {
                    _token = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly IProfileService _profileService;

        /// <summary>
        /// Конструктор AuthorizationViewModel.
        /// </summary>
        /// <param name="profileService">Сервис профиля для выполнения авторизации.</param>
        public AuthorizationViewModel(IProfileService profileService)
        {
            _profileService = profileService;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        /// <summary>
        /// Асинхронный метод для выполнения входа в систему.
        /// </summary>
        public async Task LoginAsync()
        {
            try
            {
                // Попытка получить токен авторизации
                Token = await _profileService.LoginAsync(Email, Password);

                if (string.IsNullOrEmpty(Token))
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Неверный email или пароль", "OK");
                    return;
                }

                // Сохранение токена в безопасном хранилище
                await SecureStorage.Default.SetAsync("jwt_token", Token);

                // Декодирование JWT-токена и извлечение UID пользователя
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(Token);
                var uid = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

                if (!string.IsNullOrEmpty(uid))
                {
                    try
                    {
                        // Сохранение UID пользователя в безопасном хранилище
                        await SecureStorage.Default.SetAsync("user_uid", uid);
                        AppSettings.test_user_guid = uid; // Перенос UID в тестовый набор
                        await Shell.Current.Navigation.PopModalAsync();
                        ModalClosed?.Invoke(this, EventArgs.Empty);
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
    }
}
