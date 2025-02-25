using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
using T2WebSoket.Repositories;

namespace T2WebSoket.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        public ChatService(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUserToGroupChatAsync(Guid chatId, Guid userId)
        {
            var usersChat = new UsersChats { ChatId = chatId, UserId = userId };
            _context.UsersChats.Add(usersChat);
            await _context.SaveChangesAsync();
        }

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

        public async Task<ChatDTO> CreatePrivateChatAsync(Guid userId1, Guid userId2)
        {
            var chatType = await _context.ChatTypes.SingleOrDefaultAsync(ct => ct.Name == "Private");
            var chat = new Chat { Name = $"Private Chat", TypeId = chatType.Id };
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

        public async Task<UserDTO> CreateUserAsync(Guid userId, string username)
        {
            var user = new User { Id = userId, UserName = username };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

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
