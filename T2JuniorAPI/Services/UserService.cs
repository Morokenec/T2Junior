using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.MappingProfiles;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _subscribersMapper;
        private readonly IMapper _subscriptionsMapper;

        public UserService(ApplicationDbContext context, SubscribersProfile subscribersProfile, SubscriptionsProfile subscriptionsProfile)
        {
            _context = context;
            _subscribersMapper = new MapperConfiguration(cfg => cfg.AddProfile(subscribersProfile)).CreateMapper(); ;
            _subscriptionsMapper = new MapperConfiguration(cfg => cfg.AddProfile(subscriptionsProfile)).CreateMapper();
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

        public async Task<IEnumerable<SubscriberProfileDTO>> GetSubscribers(Guid userId)
        {
            var subscribers = await _context.UserSubscribers
                .Where(us => us.IdUser == userId)
                .Include(us => us.Subscriber)
                .ToListAsync();

            return _subscribersMapper.Map<IEnumerable<SubscriberProfileDTO>>(subscribers);
        }

        public async Task<IEnumerable<SubscriberProfileDTO>> GetSubscriptions(Guid userId)
        {
            var subscriptions = await _context.UserSubscribers
                .Where(us => us.IdSubscriber == userId)
                .Include(us => us.User)
                .ToListAsync();

            return _subscriptionsMapper.Map<IEnumerable<SubscriberProfileDTO>>(subscriptions);
        }
    }
}
