using MauiApp1.Services.UseCase;
using MauiApp1.ViewModels.Profile;
using System.Text.RegularExpressions;

namespace MauiApp1
{
    /// <summary>
    /// Страница авторизации в приложении.
    /// </summary>
    public partial class AuthorizationPage : ContentPage
    {
        private bool _isVisible = false;

        private readonly UserProfileViewModel _userViewModel;

        /// <summary>
        /// Конструктор класса AuthorizationPage.
        /// </summary>
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Забыли пароль".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(_userViewModel));
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку видимости пароля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnPasswordVisibilityClicked(object sender, EventArgs e)
        {
            _isVisible = !_isVisible;
            PswdEntry.IsPassword = !_isVisible;

            TogglePasswordVisibilityButton.Source = _isVisible ? "eye_open.svg" : "eye_closed.svg";
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Войти".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnEnterClicked(object sender, EventArgs e)
        {
            string pattern = @"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[!@#$%^=&*(),.?"":{}|<>]).{8,}$";
            string phonePattern = @"^(?:\+7|8)\d{10}$";

            if (string.IsNullOrWhiteSpace(EmailEntry?.Text?.Trim()) || string.IsNullOrWhiteSpace(PswdEntry?.Text?.Trim()))
            {
                ValidationLabel.Text = "Поля не могут оставаться пустыми!";
                ValidationLabel.IsVisible = true;

            }
            else
            {
                if (Regex.IsMatch(PswdEntry.Text, pattern) && Regex.IsMatch(EmailEntry.Text, phonePattern))
                {
                    ValidationLabel.IsVisible = true;
                    ValidationLabel.Text = "Всё ништяк!";
                    ValidationLabel.TextColor = Colors.Green;
                }
                else
                {
                    ValidationLabel.IsVisible = true;
                    ValidationLabel.Text = "It's over";
                    ValidationLabel.TextColor = Colors.Red;
                }

            }
        }
    }

}
