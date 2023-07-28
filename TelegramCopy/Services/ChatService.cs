using TelegramCopy.Extensions;
using TelegramCopy.Models;

namespace TelegramCopy.Services;

public class ChatService : IChatService
{
    private readonly IRepositoryService _repositoryService;

    public ChatService(
        IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    #region -- IChatService implementation --

    public async Task<IEnumerable<ChatInfo>> GetAllChatsAsync()
    {
        var result = new List<ChatInfo>();

        try
        {
            var profilesDto = await _repositoryService.FindWhereAsync<ProfileDTO>(x => x.Id != Constants.CURRENT_USER_ID);
            var messagesDto = await _repositoryService.GetAllAsync<MessageDTO>();
            var chatsDto = await _repositoryService.GetAllAsync<ChatInfoDTO>();

            foreach (var chatDto in chatsDto)
            {
                var profile = profilesDto.FirstOrDefault(x => x.Id == chatDto.SenderProfileId).ToProfile();
                var chatMessages = messagesDto
                    .Where(x => x.ChatId == chatDto.Id)
                    .OrderByDescending(x => x.SendDate)
                    .Select(x => x.ToMessage());

                result.Add(chatDto.ToChatInfo(profile, chatMessages));
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(GetAllChatsAsync)}: {ex.Message}");
        }

        return result;
    }

    public async Task MockDatabaseAsync()
    {
        /*await _repositoryService.DeleteAllAsync<ProfileDTO>();
        await _repositoryService.DeleteAllAsync<MessageDTO>();
        await _repositoryService.DeleteAllAsync<ChatInfoDTO>();*/

        var profiles = await _repositoryService.GetAllAsync<ProfileDTO>();

        if (!profiles.Any())
        {
            await MockChatsAsync();
            await MockProfilesAsync();
            await MockMessagesAsync();
        }
    }

    private Task MockChatsAsync()
    {
        var chats = new List<ChatInfoDTO>
        {
            new() { SenderProfileId = 2 },
            new() { SenderProfileId = 3 },
            new() { SenderProfileId = 4 },
            new() { SenderProfileId = 5 },
            new() { SenderProfileId = 6 },
        };

        return _repositoryService.SaveOrUpdateRangeAsync(chats);
    }

    private Task MockProfilesAsync()
    {
        var profiles = new List<ProfileDTO>
        {
            new() { Image = "dotnet_bot", Nickname = "Dmytro Fedchenko", LastOnline = DateTime.Now.AddHours(-7) },
            new() { Image = "avatar_1.jpeg", Nickname = "Emilly Watson", LastOnline = DateTime.Now.AddHours(-20) },
            new() { Image = "avatar_2.jpeg", Nickname = "Anatoly Shpak", LastOnline = DateTime.Now.AddHours(-40) },
            new() { Image = "avatar_3.jpeg", Nickname = "Dmytro Lebovskiy", LastOnline = DateTime.Now.AddHours(-1) },
            new() { Image = "avatar_4.jpeg", Nickname = "John Smith", LastOnline = DateTime.Now.AddMinutes(-10) },
            new() { Image = "avatar_5.jpeg", Nickname = "Ivan", LastOnline = DateTime.Now },
        };

        return _repositoryService.SaveOrUpdateRangeAsync(profiles);
    }

    private Task MockMessagesAsync()
    {
        var messages = new List<MessageDTO>
        {
            new() { ChatId = 1, Content = "Hello", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 1, Content = "Are you listening?", SendDate = DateTime.Now.AddHours(-23) },
            new() { ChatId = 1, Content = "Yes", SendDate = DateTime.Now.AddHours(-22) },
            new() { ChatId = 1, Content = "So listen...", SendDate = DateTime.Now.AddHours(-21) },

            new() { ChatId = 2, Content = "Hi", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 3, Content = "Hi", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 4, Content = "Hi", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 5, Content = "Hi", SendDate = DateTime.Now.AddDays(-1) },
        };

        return _repositoryService.SaveOrUpdateRangeAsync(messages);
    }

    #endregion

    #region -- Private helpers --

    #endregion
}

