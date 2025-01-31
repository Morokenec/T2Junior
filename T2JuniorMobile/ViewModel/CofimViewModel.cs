using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace T2JuniorMobile.ViewModel
{
    public class ConfirmViewModel : BaseViewModel
    {
        private string _password;
        private string _confirmPassword;
        private Color _passwordBorderColor = Colors.Transparent;
        private Color _confirmPasswordBorderColor = Colors.Transparent;

        public ConfirmViewModel()
        {
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

        public Color PasswordBorderColor
        {
            get => _passwordBorderColor;
            set
            {
                _passwordBorderColor = value;
                OnPropertyChanged();
            }
        }

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
