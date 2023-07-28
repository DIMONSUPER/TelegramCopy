using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TelegramCopy.Models;
using TelegramCopy.Services;

namespace TelegramCopy.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly IChatService _chatService;

    public MainPageViewModel(
        IChatService chatService)
    {
        _chatService = chatService;
    }

    #region -- Public properties --

    [ObservableProperty]
    private ObservableCollection<ChatInfo> _chats = new();

    [ObservableProperty]
    private ChatInfo _selectedChat;

    [ObservableProperty]
    private Profile _currentProfile;

    [ObservableProperty]
    private string _messageText;

    public event EventHandler<bool> ScrollToLastMessage;

    #endregion

    #region -- Overrides --

    public override async void Initialize(INavigationParameters parameters)
    {
        base.Initialize(parameters);

        await _chatService.MockDatabaseAsync();

        CurrentProfile = await _chatService.GetCurrentProfileAsync();

        var chats = await _chatService.GetAllChatsAsync();

        Chats = new(chats);
    }

    #endregion

    #region -- Private helpers --

    private void RequestScrollToLastMessage(bool isAnimated = false)
    {
        ScrollToLastMessage?.Invoke(this, isAnimated);
    }

    [RelayCommand]
    private async Task SendMessageTapped()
    {
        var message = new Message
        {
            ChatId = SelectedChat.Id,
            SendDate = DateTime.Now,
            Content = MessageText,
            IsMyMessage = true,
        };

        var savedMessage = await _chatService.SendMessageAsync(message);

        if (savedMessage is not null)
        {
            SelectedChat.AddMessage(savedMessage);
            MessageText = string.Empty;
            RequestScrollToLastMessage(true);
        }

        _ = GenerateAnswerAsync();
    }

    private Task GenerateAnswerAsync()
    {
        return Task.Run(async () =>
        {
            var oldSelectedChat = SelectedChat;
            await Task.Delay(new Random().Next(2000, 10000));

            var message = new Message
            {
                ChatId = oldSelectedChat.Id,
                SendDate = DateTime.Now,
                Content = "Answering you",
                IsMyMessage = false,
            };

            var savedMessage = await _chatService.SendMessageAsync(message);

            if (savedMessage is not null)
            {
                oldSelectedChat.AddMessage(savedMessage);

                if (SelectedChat == oldSelectedChat)
                {
                    RequestScrollToLastMessage(true);
                }
                else
                {
                    oldSelectedChat.UnreadMessagesCount++;
                    var result = await _chatService.UpdateChatAsync(oldSelectedChat);
                }
            }
        });
    }

    [RelayCommand]
    private async Task ChatTapped(ChatInfo tappedChat)
    {
        if (SelectedChat is not null)
        {
            SelectedChat.IsSelected = false;
        }

        if (tappedChat is not null)
        {
            SelectedChat = tappedChat;
            SelectedChat.IsSelected = true;
            MessageText = string.Empty;
            RequestScrollToLastMessage();

            if (SelectedChat.UnreadMessagesCount > 0)
            {
                SelectedChat.UnreadMessagesCount = 0;
                await _chatService.UpdateChatAsync(SelectedChat);
            }
        }
    }

    #endregion
}

