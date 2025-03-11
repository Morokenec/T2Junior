using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.MappingProfiles;
using T2JuniorAPI.Entities;
using T2JuniorAPI.DTOs.Users;

namespace T2JuniorAPI.Services.Users
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _subscribersMapper;
        private readonly IMapper _subscriptionsMapper;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для внедрения зависимостей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="subscribersProfile">Профиль маппера для работы с подписчиками</param>
        /// <param name="subscriptionsProfile">Профиль маппера для работы с подписками</param>
        /// <param name="mapper">Основной маппер</param>
        public UserService(ApplicationDbContext context, SubscribersProfile subscribersProfile, SubscriptionsProfile subscriptionsProfile, IMapper mapper)
        {
            _context = context;
            _subscribersMapper = new MapperConfiguration(cfg => cfg.AddProfile(subscribersProfile)).CreateMapper(); ;
            _subscriptionsMapper = new MapperConfiguration(cfg => cfg.AddProfile(subscriptionsProfile)).CreateMapper();
            _mapper = mapper;
        }

        /// <summary>
        /// Подписывает пользователя на другого пользователя
        /// </summary>
        /// <param name="subscribeUser">Данные о подписчике и подписке</param>
        /// <returns>Сообщение о результате операции</returns>
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

            var userSubscriber = _mapper.Map<UserSubscribers>(subscribeUser);

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

        /// <summary>
        /// Отписывает пользователя от другого пользователя
        /// </summary>
        /// <param name="unsubscribeUser">Данные о отписке</param>
        /// <returns>Сообщение о результате операции</returns>
        public async Task<string> UnsubscribeUserFromUser(UnsubscribeUserDTO unsubscribeUser)
        {
            var userSubscriber = await _context.UserSubscribers
                .FirstOrDefaultAsync(us => us.IdUser == unsubscribeUser.SubscriptionId && us.IdSubscriber == unsubscribeUser.UserId && !us.IsDelete);

            if (userSubscriber == null)
                return "Subscription not found";

            userSubscriber.IsDelete = true;
            userSubscriber.UpdateDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return "User successfully unsubscribed";
        }

        /// <summary>
        /// Возвращает список подписчиков указанного пользователя
        /// </summary>
        /// <param name="userId">Id пользователя, чьи подписчики требуется получить</param>
        /// <returns>Коллекция подписчиков</returns>
        public async Task<IEnumerable<SubscriberProfileDTO>> GetSubscribers(Guid userId)
        {
            var subscribers = await _context.UserSubscribers
                .Where(us => us.IdUser == userId)
                .Include(us => us.Subscriber)
                .ToListAsync();

            return _subscribersMapper.Map<IEnumerable<SubscriberProfileDTO>>(subscribers);
        }

        /// <summary>
        /// Возвращает список подписок указанного пользователя
        /// </summary>
        /// <param name="userId">Id пользователя, чьи подписки требуется получить</param>
        /// <returns>Коллекция подписок</returns>
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
