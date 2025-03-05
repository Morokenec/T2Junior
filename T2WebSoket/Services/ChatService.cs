using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
using T2WebSoket.Repositories;

namespace T2WebSoket.Services
{
    /// <summary>
    /// Сервис для управления чатами.
    /// </summary>
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса ChatService.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с чатами.</param>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        public ChatService(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавление пользователя в групповой чат.
        /// </summary>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task AddUserToGroupChatAsync(Guid chatId, Guid userId)
        {
            var usersChat = new UsersChats { ChatId = chatId, UserId = userId };
            _context.UsersChats.Add(usersChat);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Создание группового чата.
        /// </summary>
        /// <param name="creatorUserId">Идентификатор создателя чата.</param>
        /// <param name="chatName">Название чата.</param>
        /// <returns>Созданный групповой чат.</returns>
        public async Task<ChatDTO> CreateGroupChatAsync(Guid creatorUserId, string chatName)
        {
            var chatType = await _context.ChatTypes.SingleOrDefaultAsync(ct => ct.Name == "Group");
            var chat = new Chat { Name = chatName, TypeId = chatType.Id };
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            var usersChat = new UsersChats { ChatId = chat.Id, UserId = creatorUserId };
            _context.UsersChats.Add(usersChat);
            await _context.SaveChangesAsync();

            return _mapper.Map<ChatDTO>(chat);
        }

        /// <summary>
        /// Создание приватного чата между двумя пользователями.
        /// </summary>
        /// <param name="userId1">Идентификатор первого пользователя.</param>
        /// <param name="userId2">Идентификатор второго пользователя.</param>
        /// <returns>Созданный приватный чат.</returns>
        public async Task<ChatDTO> CreatePrivateChatAsync(Guid userId1, Guid userId2)
        {
            var user1 = await _context.Users.FindAsync(userId1);
            var user2 = await _context.Users.FindAsync(userId2);
            if ((user1 == null && !user1.IsDelete) || (user2 == null && !user2.IsDelete))
                throw new ApplicationException("Users not found");

            var chatType = await _context.ChatTypes.SingleOrDefaultAsync(ct => ct.Name == "Private");
            var chat = new Chat { Name = $"{user1.UserName}-{user2.UserName}", TypeId = chatType.Id };
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            var usersChats = new List<UsersChats>
            {
                new UsersChats {ChatId = chat.Id, UserId = userId1},
                new UsersChats {ChatId = chat.Id, UserId = userId2}
            };
            _context.UsersChats.AddRange(usersChats);
            await _context.SaveChangesAsync();

            return _mapper.Map<ChatDTO>(chat);
        }

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        public async Task<UserDTO> CreateUserAsync(Guid userId, string username)
        {
            var user = new User { Id = userId, UserName = username };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Получение чатов пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список чатов пользователя.</returns>
        public async Task<ICollection<ChatDTO>> GetChatsByUser(Guid userId)
        {
            var userChats = await _context.UsersChats
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Chat)
                    .ThenInclude(c => c.Messages)
                .ToListAsync();

            return _mapper.Map<ICollection<ChatDTO>>(userChats.Select(uc => uc.Chat));
        }
    }
}
