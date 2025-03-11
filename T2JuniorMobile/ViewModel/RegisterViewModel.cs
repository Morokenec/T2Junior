using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace T2JuniorMobile.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        public ObservableCollection<string> GenderOptions { get; set; }
        public ObservableCollection<string> OrganizationOptions { get; set; }

        public string FullName { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public bool SelectedGender { get; set; }
        public string SelectedOrganization { get; set; }

        public ICommand NavigateConfimCommand { get; }

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
