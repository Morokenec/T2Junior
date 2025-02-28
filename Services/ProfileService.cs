using MauiApp1.DataModel;
using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public static class ProfileService
    {
        public static UserProfileDTO SelectedUser { get; set; }
        public static Club SelectedClub { get; set; }
    }
}
