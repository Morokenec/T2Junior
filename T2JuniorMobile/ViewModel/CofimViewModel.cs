using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace T2JuniorMobile.ViewModel
{
    /// <summary>
    /// ViewModel для управления подтверждением пароля.
    /// </summary>
    public class ConfirmViewModel : BaseViewModel
    {
        private string _password;
        private string _confirmPassword;
        private Color _passwordBorderColor = Colors.Transparent;
        private Color _confirmPasswordBorderColor = Colors.Transparent;

        /// <summary>
        /// Конструктор класса ConfirmViewModel.
        /// </summary>
        public ConfirmViewModel()
        {
        }

        /// <summary>
        /// Пароль пользователя.
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
        /// Подтверждение пароля пользователя.
        /// </summary>
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Цвет границы поля ввода пароля.
        /// </summary>
        public Color PasswordBorderColor
        {
            get => _passwordBorderColor;
            set
            {
                _passwordBorderColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Цвет границы поля ввода подтверждения пароля.
        /// </summary>
        public Color ConfirmPasswordBorderColor
        {
            get => _confirmPasswordBorderColor;
            set
            {
                _confirmPasswordBorderColor = value;
                OnPropertyChanged();
            }
        }
    }
}
