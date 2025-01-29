using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Models;

namespace T2JuniorAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> SubscribeUserToUser(SubscribeUserDTO subscribeUser)
        {
            var user = await _context.Users.FindAsync(subscribeUser.UserId);
            var subscriber = await _context.Users.FindAsync(subscribeUser.SubscriberId);
            if (user == null)
            {
                return "User not found";
            }
            if (subscriber == null)
            {
                return "Subscriber not found";
            }

            var userSubscriber = new UserSubscribers
            {
                IdUser = subscribeUser.UserId,
                IdSubscriber = subscribeUser.SubscriberId,
            };

            await _context.UserSubscribers.AddAsync(userSubscriber);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "User alredy subscribed";
            }

            return "User successfully subscribe";
        }

        public async Task<IEnumerable<SubscriberProfileDTO>> GetSubscribers(string userId)
        {
            var subscribers = await _context.UserSubscribers
                .Where(us => us.IdUser == userId)
                .Select(us => new SubscriberProfileDTO
                {
                    Id = us.Subscriber.Id,
                    FullName = $"{us.Subscriber.FirstName} {us.Subscriber.LastName}"
                })
                .ToListAsync();

            return subscribers;
        }

        public async Task<IEnumerable<SubscriberProfileDTO>> GetSubscriptions(string userId)
        {
            var subscriptions = await _context.UserSubscribers
                .Where(us => us.IdSubscriber == userId)
                .Select(us => new SubscriberProfileDTO
                {
                    Id = us.User.Id,
                    FullName = us.User.FirstName + " " + us.User.LastName
                })
                .ToListAsync();

            return subscriptions;
        }
    }
}
