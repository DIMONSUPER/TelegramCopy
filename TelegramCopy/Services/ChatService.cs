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

    public async Task<ChatInfo> UpdateChatAsync(ChatInfo chat)
    {
        ChatInfo result = null;

        try
        {
            var id = await _repositoryService.SaveOrUpdateAsync(chat.ToDTO());

            if (id != -1)
            {
                result = (await _repositoryService.GetSingleByIdAsync<ChatInfoDTO>(id))?.ToChatInfo(chat.Profile, chat.Messages);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Message was not saved");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(SendMessageAsync)}: {ex.Message}");
        }

        return result;
    }

    //TODO: move to profile service
    public async Task<Profile> GetCurrentProfileAsync()
    {
        Profile result = null;

        try
        {
            result = (await _repositoryService.GetSingleByIdAsync<ProfileDTO>(Constants.CURRENT_USER_ID))?.ToProfile();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(GetCurrentProfileAsync)}: {ex.Message}");
        }

        return result;
    }

    //TODO: move to message service
    public async Task<Message> SendMessageAsync(Message message)
    {
        Message result = null;

        try
        {
            var id = await _repositoryService.SaveAsync(message.ToDTO());

            if (id != -1)
            {
                result = (await _repositoryService.GetSingleByIdAsync<MessageDTO>(id))?.ToMessage();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Message was not saved");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(SendMessageAsync)}: {ex.Message}");
        }

        return result;
    }

    //TODO: move to mock service
    public async Task MockDatabaseAsync()
    {
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
            new() { Image = "avatar_0.png", Nickname = "Dmytro Fedchenko", LastOnline = DateTime.Now.AddHours(-7) },
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
            new() { ChatId = 1, IsMyMessage = false, Content = "Hello", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 1, IsMyMessage = false, Content = "Are you listening?", SendDate = DateTime.Now.AddHours(-3) },
            new() { ChatId = 1, IsMyMessage = true, Content = "Yes", SendDate = DateTime.Now.AddHours(-2) },
            new() { ChatId = 1, IsMyMessage = false, Content = "So listen...", SendDate = DateTime.Now.AddHours(-1) },

            new() { ChatId = 2, IsMyMessage = false, Content = "Hi", SendDate = DateTime.Now.AddDays(-1) },
            new() { ChatId = 3, IsMyMessage = false, Content = "Hi", SendDate = DateTime.Now.AddDays(-4) },
            new() { ChatId = 4, IsMyMessage = false, Content = "Hi", SendDate = DateTime.Now.AddDays(-2) },
            new() { ChatId = 5, IsMyMessage = false, Content = "Hi", SendDate = DateTime.Now.AddDays(-3) },
        };

        return _repositoryService.SaveOrUpdateRangeAsync(messages);
    }

    #endregion

    #region -- Private helpers --

    #endregion
}

