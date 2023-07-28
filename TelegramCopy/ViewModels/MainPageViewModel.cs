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
    private string _messageText;

    #endregion

    #region -- Overrides --

    public override async void Initialize(INavigationParameters parameters)
    {
        base.Initialize(parameters);

        await _chatService.MockDatabaseAsync();

        var chats = await _chatService.GetAllChatsAsync();

        Chats = new(chats);
    }

    #endregion

    #region -- Private helpers --

    [RelayCommand]
    private void ChatTapped(ChatInfo tappedChat)
    {
        if (SelectedChat is not null)
        {
            SelectedChat.IsSelected = false;
        }

        if (tappedChat is not null)
        {
            SelectedChat = tappedChat;
            SelectedChat.IsSelected = true;
        }
    }

    #endregion
}

