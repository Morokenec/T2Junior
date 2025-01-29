using T2JuniorAPI.DTOs;

namespace T2JuniorAPI.Services
{
    public interface IUserService
    {
        Task<string> SubscribeUserToUser(SubscribeUserDTO subscribeUser);
        Task<IEnumerable<SubscriberProfileDTO>> GetSubscribers(string userId);
        Task<IEnumerable<SubscriberProfileDTO>> GetSubscriptions(string userId);

    }
}
