using MauiApp1.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.UseCase.Interface
{
    public interface IProfileService
    {
        Task<TaiyoResponse> GetProfileDataAsync();
    }
}
