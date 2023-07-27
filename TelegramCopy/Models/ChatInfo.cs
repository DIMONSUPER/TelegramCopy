using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.Models;

public partial class ChatInfo : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string _image;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private Message _lastMessageId;

    [ObservableProperty]
    private bool _isSelected;
}

