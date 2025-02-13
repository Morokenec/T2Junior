using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.ViewModels
{
    public class UserViewModel : BindableObject
    {
        public ObservableCollection<UserProfileDTO> UserProfiles { get; set; }
        public UserViewModel() 
        {
            UserProfiles = new ObservableCollection<UserProfileDTO>
        {
            new UserProfileDTO { IdUser = 1, FirstName = "Дмитрий", LastName = "Ушаков"},
            new UserProfileDTO { IdUser = 2, FirstName = "Виталий", LastName = "Таран"},
            new UserProfileDTO { IdUser = 3, FirstName = "Матвей", LastName = "Абрамов"},
            new UserProfileDTO { IdUser = 4, FirstName = "Тимофей", LastName = "Багин" },
            new UserProfileDTO { IdUser = 5, FirstName = "Дмитрий", LastName = "Ушаков" }
        };
        }
        public UserProfileDTO GetProfileById(int idUser)
        {
            return UserProfiles.FirstOrDefault(up => up.IdUser == idUser);
        }
    }
}
