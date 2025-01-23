using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace T2JuniorMobile.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private string _fullName;
        private string _email;
        private string _numberPhone;
        private DateTime _date;

        public ICommand NavigateConfimCommand { get; }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string NumberPhone
        {
            get => _numberPhone;
            set => SetProperty(ref _numberPhone, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public RegisterViewModel()
        {
            NavigateConfimCommand = new Command(async () => await NavigateConfimCommandAsync());
        }

        private async Task NavigateConfimCommandAsync()
        {
            await Shell.Current.GoToAsync("/ConfimPage");
        }
    }
}
