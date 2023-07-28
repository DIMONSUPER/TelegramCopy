using TelegramCopy.Models;

namespace TelegramCopy.Services;

public interface IChatService
{
    Task<IEnumerable<ChatInfo>> GetAllChatsAsync();

    Task MockDatabaseAsync();
}

