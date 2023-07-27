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

    #endregion

    #region -- Overrides --

    public override void Initialize(INavigationParameters parameters)
    {
        base.Initialize(parameters);

        MockChats();
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

    private void MockChats()
    {
        Chats = new ObservableCollection<ChatInfo>
        {
            new ChatInfo() { Id = 1, Image = "dotnet_bot", Name = "Chat 1" },
            new ChatInfo() { Id = 1, Image = "dotnet_bot", Name = "Chat 1" },
            new ChatInfo() { Id = 1, Image = "dotnet_bot", Name = "Chat 1" },
            new ChatInfo() { Id = 1, Image = "dotnet_bot", Name = "Chat 1" },
            new ChatInfo() { Id = 1, Image = "dotnet_bot", Name = "Chat 1" },
        };
    }

    #endregion
}

