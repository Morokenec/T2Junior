using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
using T2WebSoket.Repositories;

namespace T2WebSoket.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDbContext _context;

        public ChatHub(ChatDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<MessageDTO>> GetChatHistory(Guid chatId)
        {
            var chatMessages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreationDate)
                .Include(m => m.User)
                .Select(m => new MessageDTO
                {
                    Body = m.Body,
                    CreationDate = m.CreationDate,
                    UserName = m.User.UserName,
                })
                .ToListAsync();

            return chatMessages;
        }

        public async Task Send(string userId, string message, string chatId)
        {
            Console.WriteLine("we a enter in the server method");
            try
            {
                var user = await _context.Users.FindAsync(Guid.Parse(userId));
                Console.WriteLine("try user check");
                if (user == null)
                {
                    Console.Error.WriteLine($"User not found: {userId}");
                    throw new Exception($"User not found: {userId}");
                }
                Console.WriteLine("user check success!\n try chat check");

                var chat = await _context.Chats.FindAsync(Guid.Parse(chatId));
                if (chat == null)
                {
                    Console.Error.WriteLine($"Chat not found: {chatId}");
                    throw new Exception($"Chat not found: {chatId}");
                }
                Console.WriteLine("chat Check success!");

                var chatMessage = new Message
                {
                    UserId = user.Id,
                    ChatId = chat.Id,
                    Body = message,
                    MediaUrl = null
                };

                _context.Messages.Add(chatMessage);
                await _context.SaveChangesAsync();
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.UserName, message, chatMessage.CreationDate, null);
            }
            catch (Exception ex)
            {
                // Логирование ошибки для отладки
                Console.Error.WriteLine($"Error sending message: {ex.Message}");
                throw; // Можно выбросить исключение, чтобы клиент знал о проблеме
            }
        }


        public async Task JoinGroup(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveGroup(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
