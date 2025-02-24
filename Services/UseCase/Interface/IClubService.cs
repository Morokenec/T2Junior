using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.UseCase.Interface
{
    public interface IClubService
    {
        Task<Club> GetClubById(Guid idClub);
        Task<List<ClubList>> GetClubsAsync();
        Task SubscribeClub(Guid clubId, Guid userId, Guid roleId);
    }
}
