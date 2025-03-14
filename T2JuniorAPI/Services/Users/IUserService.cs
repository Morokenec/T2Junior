﻿using T2JuniorAPI.DTOs.Users;

namespace T2JuniorAPI.Services.Users
{
    public interface IUserService
    {
        Task<string> SubscribeUserToUser(SubscribeUserDTO subscribeUser);
        Task<string> UnsubscribeUserFromUser(UnsubscribeUserDTO unsubscribeUser);
        Task<IEnumerable<SubscriberProfileDTO>> GetSubscribers(Guid userId);
        Task<IEnumerable<SubscriberProfileDTO>> GetSubscriptions(Guid userId);

    }
}
