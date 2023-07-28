using TelegramCopy.Models;

namespace TelegramCopy.Services;

public interface IChatService
{
    Task<IEnumerable<ChatInfo>> GetAllChatsAsync();

    Task<ChatInfo> UpdateChatAsync(ChatInfo chat);

    Task<Message> SendMessageAsync(Message message);

    //TODO: move to profile service
    Task<Profile> GetCurrentProfileAsync();

    //TODO: move to mock service
    Task MockDatabaseAsync();
}

