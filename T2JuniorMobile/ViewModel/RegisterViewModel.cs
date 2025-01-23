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
        public RegisterViewModel()
        {
            NavigateConfimCommand = new Command(async () => await NavigateConfimCommandAsync());
        }

        public ICommand NavigateConfimCommand { get; }

        private async Task NavigateConfimCommandAsync()
        {
            await Shell.Current.GoToAsync("/ConfimPage");
        }
    }
}
