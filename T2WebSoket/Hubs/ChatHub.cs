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

        /// <summary>
        /// Конструктор класса ChatHub.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с чатами.</param>
        /// <param name="fileService">Сервис для работы с файлами.</param>
        public ChatHub(ChatDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        /// <summary>
        /// Получение истории сообщений чата.
        /// </summary>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <returns>Список сообщений чата.</returns>
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

        /// <summary>
        /// Отправка текстового сообщения.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        public async Task Send(string userId, string message, string chatId)
        {
            try
            {
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

                var chatMessage = new Message
                {
                    UserId = user.Id,
                    ChatId = chat.Id,
                    Text = message,
                };

                _context.Messages.Add(chatMessage);
                await _context.SaveChangesAsync();

                // Отправка сообщения всем участникам группы
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.UserName, message, chatMessage.CreationDate);
            }
            catch (Exception ex)
            {
                // Логирование ошибки для отладки
                Console.Error.WriteLine($"Error sending message: {ex.Message}");
                throw; // Можно выбросить исключение, чтобы клиент знал о проблеме
            }
        }

        /// <summary>
        /// Присоединение клиента к группе чата.
        /// </summary>
        /// <param name="chatId">Идентификатор чата.</param>
        public async Task JoinGroup(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        /// <summary>
        /// Удаление клиента из группы чата.
        /// </summary>
        /// <param name="chatId">Идентификатор чата.</param>
        public async Task LeaveGroup(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        /// <summary>
        /// Определение типа файла по его расширению.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Тип файла (image, video, file).</returns>
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

        /// <summary>
        /// Отправка уведомления о новом сообщении с файлом.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <param name="filePath">Путь к файлу.</param>
        public async Task SendWithFileNotification(string userId, string message, string chatId, string fileId, string filePath)
        {
            try
            {
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

    }
}
