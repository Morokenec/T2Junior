using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2WebSoket.DTOs;
using T2WebSoket.Services;

namespace T2WebSoket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        /// <summary>
        /// Конструктор класса ChatController.
        /// </summary>
        /// <param name="chatService">Сервис для работы с чатами.</param>
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        [HttpPost("users")]
        public async Task<ActionResult<UserDTO>> CreateUser(Guid userId, string userName)
        {
            var user = await _chatService.CreateUserAsync(userId, userName);
            return Ok(user);
        }

        /// <summary>
        /// Создание приватного чата между двумя пользователями.
        /// </summary>
        /// <param name="userId1">Идентификатор первого пользователя.</param>
        /// <param name="userId2">Идентификатор второго пользователя.</param>
        /// <returns>Созданный приватный чат.</returns>
        [HttpPost("private")]
        public async Task<ActionResult<ChatDTO>> CreatePrivateChat(Guid userId1, Guid userId2)
        {
            var chat = await _chatService.CreatePrivateChatAsync(userId1, userId2);
            return Ok(chat);
        }

        /// <summary>
        /// Создание группового чата.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, создающего чат.</param>
        /// <param name="chatName">Название чата.</param>
        /// <returns>Созданный групповой чат.</returns>
        [HttpPost("group")]
        public async Task<ActionResult<ChatDTO>> CreateGroupChat(Guid userId, string chatName)
        {
            var chat = await _chatService.CreateGroupChatAsync(userId, chatName);
            return Ok(chat);
        }

        /// <summary>
        /// Добавление пользователя в групповой чат.
        /// </summary>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="UserId">Идентификатор пользователя.</param>
        /// <returns>Результат добавления пользователя в чат.</returns>
        [HttpPost("group/{chatId}/users")]
        public async Task<IActionResult> AddUserToGroupChat(Guid chatId, Guid UserId)
        {
            await _chatService.AddUserToGroupChatAsync(chatId, UserId);
            return Ok();
        }

        /// <summary>
        /// Получение списка чатов для пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список чатов пользователя.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetChats(Guid userId)
        {
            var chats = await _chatService.GetChatsByUser(userId);
            return Ok(chats);
        }
    }
}
