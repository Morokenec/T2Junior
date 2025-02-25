using T2WebSoket.DTOs;

namespace T2WebSoket.Services
{
    public interface IChatService
    {
        Task<UserDTO> CreateUserAsync(Guid userId, string username);
        Task<ChatDTO> CreatePrivateChatAsync(Guid userId1, Guid userId2);
        Task<ChatDTO> CreateGroupChatAsync(Guid creatorUserId, string chatName);
        Task AddUserToGroupChatAsync(Guid chatId, Guid userId);
        Task<ICollection<ChatDTO>> GetChatsByUser(Guid userId);
    }
}
