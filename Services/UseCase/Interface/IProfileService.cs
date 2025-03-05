using MauiApp1.Models.Profile;
using MauiApp1.Models.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.UseCase.Interface
{
    public interface IProfileService
    {
        Task<ProfileResponse> GetProfileDataAsync();
        Task SetAvatarProfileUploadServer(Guid userId, Stream chosenImage);
        Task<string?> LoginAsync(string email, string password);
        Task<List<UserSocial>> GetUserSubscribers(Guid userId);
    }
}
