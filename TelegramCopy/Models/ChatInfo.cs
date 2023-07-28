using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TelegramCopy.Helpers;

namespace TelegramCopy.Models;

public partial class ChatInfo : ObservableObject
{
    public int Id { get; set; }

    public ObservableCollection<MessageGroup> MessageGroups =>
        new(Messages?.OrderBy(x => x.SendDate)
            .GroupBy(x => DateTimeHelper.CovertToNormalizedDate(x.SendDate))
            .Select(x => new MessageGroup(x.Key, x)));

    [ObservableProperty]
    private Profile _profile;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MessageGroups))]
    private ObservableCollection<Message> _messages;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private int _unreadMessagesCount;

    #region -- Public helpers --

    public void AddMessage(Message message)
    {
        Messages.Add(message);
        OnPropertyChanged(nameof(MessageGroups));
    }

    #endregion
}

