using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2JuniorMobile.Services;

namespace T2JuniorMobile.ViewModel
{
    /// <summary>
    /// ViewModel для управления данными профиля пользователя.
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        private string _name;
        private string _role;
        private string _organization;
        private  readonly ProfileServices _profileServices;

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Роль пользователя в организации.
        /// </summary>
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        /// <summary>
        /// Организация или принадлежность пользователя.
        /// </summary>
        public string Organization
        {
            get => _organization;
            set => SetProperty(ref _organization, value);
        }

        /// <summary>
        /// Конструктор класса ProfileViewModel.
        /// </summary>
        /// <param name="profileServices">Сервис для работы с профилем пользователя.</para
        public ProfileViewModel(ProfileServices profileServices)
        {
            _profileServices = profileServices ?? throw new ArgumentNullException(nameof(profileServices));
            LoadProfileData();
        }

        /// <summary>
        /// Загружает данные профиля пользователя асинхронно.
        /// </summary>
        public async Task LoadProfileData()
        {
            try
            {
                var profile = await _profileServices.GetProfileAsync();
                if (profile != null)
                {
                    Name = profile.FullName;
                    Role = profile.RoleName;
                    Organization = profile.PostAndOrganization;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке профиля: {ex.Message}");
            }
        }
    }
}
