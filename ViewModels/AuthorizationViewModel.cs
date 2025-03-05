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
    public class AuthorizationViewModel : BindableObject, INotifyPropertyChanged
    {
        public ICommand LoginCommand { get; }
        public event EventHandler ModalClosed;

        private string _email;
        private string _password;
        private string _token;

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

        public AuthorizationViewModel(IProfileService profileService)
        {
            _profileService = profileService;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        public async Task LoginAsync()
        {
            try
            {
                Token = await _profileService.LoginAsync(Email, Password);

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
                        AppSettings.test_user_guid = uid;
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
