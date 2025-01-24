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
                    ValidatePasswords();
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
                    ValidatePasswords();
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

        public ICommand ConfirmCommand { get; }


        private void ValidatePasswords()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                PasswordBorderColor = Colors.Red;
                ConfirmPasswordBorderColor = Colors.Red;
                ((Command)ConfirmCommand).ChangeCanExecute();
                return;
            }

            if (Password != ConfirmPassword)
            {
                PasswordBorderColor = Colors.Red;
                ConfirmPasswordBorderColor = Colors.Red;
                ((Command)ConfirmCommand).ChangeCanExecute();
            }
            else
            {
                PasswordBorderColor = Colors.Transparent;
                ConfirmPasswordBorderColor = Colors.Transparent;
                ((Command)ConfirmCommand).ChangeCanExecute();
            }
        }

        private bool CanConfirm()
        {
            return !string.IsNullOrWhiteSpace(Password) && Password == ConfirmPassword;
        }

        private void OnConfirm()
        {
            Application.Current.MainPage.DisplayAlert("Успех", "Пароли совпадают!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
