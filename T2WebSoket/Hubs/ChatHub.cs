using Microsoft.AspNetCore.SignalR;
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

        public async Task SendMessage(string userName, string message, Guid chatId, string mediaUrl = null)
        {
            var chatMessage = new Message
            {
                UserId = chatId,
                ChatId = chatId,
                Body = message,
                MediaUrl = mediaUrl
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userName, message, chatMessage.CreationDate, mediaUrl);
        }
    }
}
