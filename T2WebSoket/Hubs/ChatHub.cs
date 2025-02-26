using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
using T2WebSoket.Repositories;
using T2WebSoket.Services;

namespace T2WebSoket.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDbContext _context;
        private readonly IFileService _fileService;

        public ChatHub(ChatDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<List<MessageDTO>> GetChatHistory(Guid chatId)
        {
            var chatMessages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreationDate)
                .Include(m => m.User)
                .Select(m => new MessageDTO
                {
                    Text = m.Text,
                    CreationDate = m.CreationDate,
                    UserName = m.User.UserName,
                    FilePath = _context.MessageFiles
                        .Where(mf => mf.IdMessage == m.Id)
                        .Select(mf => mf.File.FilePath)
                        .FirstOrDefault(),
                    FileType = _context.MessageFiles
                        .Where(mf => mf.IdMessage == m.Id)
                            .Select(mf => GetFileType(mf.File.FilePath))
                            .FirstOrDefault()
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
                    Text = message,
                };

                _context.Messages.Add(chatMessage);
                await _context.SaveChangesAsync();
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.UserName, message, chatMessage.CreationDate);
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

        private static string GetFileType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return "image";
                case ".mp4":
                case ".avi":
                case ".mkv":
                    return "video";
                default:
                    return "file";
            }
        }
        public async Task SendWithFileNotification(string userId, string message, string chatId, string fileId, string filePath)
        {
            try
            {
                Console.WriteLine("Welcome to SendWithFileNotification.");
                Console.WriteLine($"Received parameters - userId: {userId}, chatId: {chatId}, fileId: {fileId}, filePath: {filePath}");

                var user = await _context.Users.FindAsync(Guid.Parse(userId));
                if (user == null)
                {
                    throw new Exception($"User not found: {userId}");
                }

                var chat = await _context.Chats.FindAsync(Guid.Parse(chatId));
                if (chat == null)
                {
                    throw new Exception($"Chat not found: {chatId}");
                }

                string fileType = GetFileType(filePath);

                // Создание сообщения
                var chatMessage = new Message
                {
                    UserId = user.Id,
                    ChatId = chat.Id,
                    Text = message,
                };

                _context.Messages.Add(chatMessage);
                await _context.SaveChangesAsync();

                // Создание связи между сообщением и файлом
                var messageFile = new MessageFile
                {
                    IdMessage = chatMessage.Id,
                    IdFile = Guid.Parse(fileId),
                };

                _context.MessageFiles.Add(messageFile);
                await _context.SaveChangesAsync();

                // Отправка сообщения клиентам
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.UserName, message, chatMessage.CreationDate, filePath, fileType);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending message with file notification: {ex.Message}");
                throw;
            }
        }


        //public async Task SendWithFile(string userId, string message, string chatId, IFormFile file)
        //{
        //    try
        //    {
        //        var user = await _context.Users.FindAsync(Guid.Parse(userId));
        //        if (user == null)
        //        {
        //            throw new Exception($"User not found: {userId}");
        //        }

        //        var chat = await _context.Chats.FindAsync(Guid.Parse(chatId));
        //        if (chat == null)
        //        {
        //            throw new Exception($"Chat not found: {chatId}");
        //        }

        //        // Загрузка файла
        //        var fileUploadDto = new FileUploadDTO
        //        {
        //            File = file,
        //            IdUser = userId
        //        };

        //        var fileDto = await _fileService.UploadFileAsync(fileUploadDto);

        //        string fileType = GetFileType(fileDto.FilePath);

        //        // Создание сообщения
        //        var chatMessage = new Message
        //        {
        //            UserId = user.Id,
        //            ChatId = chat.Id,
        //            Text = message,
        //        };

        //        _context.Messages.Add(chatMessage);
        //        await _context.SaveChangesAsync();

        //        // Создание связи между сообщением и файлом
        //        var messageFile = new MessageFile
        //        {
        //            IdMessage = chatMessage.Id,
        //            IdFile = fileDto.Id,
        //        };

        //        _context.MessageFiles.Add(messageFile);
        //        await _context.SaveChangesAsync();

        //        // Отправка сообщения клиентам
        //        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.UserName, message, chatMessage.CreationDate, fileDto.FilePath, fileType);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error sending message with file: {ex.Message}");
        //        throw;
        //    }
        //}

    }
}
