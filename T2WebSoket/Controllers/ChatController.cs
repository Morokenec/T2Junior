using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using T2WebSoket.DTOs;
using T2WebSoket.Services;

namespace T2WebSoket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("users")]
        public async Task<ActionResult<UserDTO>> CreateUser(Guid userId, string userName)
        {
            var user = await _chatService.CreateUserAsync(userId, userName);
            return Ok(user);
        }

        [HttpPost("private")]
        public async Task<ActionResult<ChatDTO>> CreatePrivateChat(Guid userId1, Guid userId2)
        {
            var chat = await _chatService.CreatePrivateChatAsync(userId1, userId2);
            return Ok(chat);
        }

        [HttpPost("group")]
        public async Task<ActionResult<ChatDTO>> CreateGroupChat(Guid userId, string chatName)
        {
            var chat = await _chatService.CreateGroupChatAsync(userId, chatName);
            return Ok(chat);
        }

        [HttpPost("group/{chatId}/users")]
        public async Task<IActionResult> AddUserToGroupChat(Guid chatId, Guid UserId)
        {
            await _chatService.AddUserToGroupChatAsync(chatId, UserId);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetChats(Guid userId)
        {
            var chats = await _chatService.GetChatsByUser(userId);
            return Ok(chats);
        }
    }
}
