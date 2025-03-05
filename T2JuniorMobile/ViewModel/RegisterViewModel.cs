using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace T2JuniorMobile.ViewModel
{
    /// <summary>
    /// ViewModel для управления процессом регистрации пользователя.
    /// </summary>
    public partial class RegisterViewModel : BaseViewModel
    {
        /// <summary>
        /// Варианты пола.
        /// </summary>
        public ObservableCollection<string> GenderOptions { get; set; }

        /// <summary>
        /// Варианты организаций.
        /// </summary>
        public ObservableCollection<string> OrganizationOptions { get; set; }

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string NumberPhone { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пол, выбранный пользователем.
        /// </summary>
        public bool SelectedGender { get; set; }

        /// <summary>
        /// Организация, выбранная пользователем.
        /// </summary>
        public string SelectedOrganization { get; set; }

        /// <summary>
        /// Команда для перехода к странице подтверждения.
        /// </summary>
        public ICommand NavigateConfirmCommand { get; }

        /// <summary>
        /// Конструктор класса RegisterViewModel.
        /// </summary>
        public RegisterViewModel()
        {
            NavigateConfirmCommand = new Command(async () => await NavigateConfirmCommandAsync());
        }

        /// <summary>
        /// Метод для перехода к странице подтверждения.
        /// </summary>
        private async Task NavigateConfirmCommandAsync()
        {
            await Shell.Current.GoToAsync("/ConfirmPage");
        }
    }
}
