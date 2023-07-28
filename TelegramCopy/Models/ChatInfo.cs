using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.Models;

public partial class ChatInfo : ObservableObject
{
    public int Id { get; set; }

    public ObservableCollection<MessageGroup> MessageGroups =>
        new(Messages?.GroupBy(x => x.SendDate)
            .OrderByDescending(x => x.Key)
            .Select(x => new MessageGroup(x.Key, x)));

    public Message LastMessage => Messages?.LastOrDefault();

    [ObservableProperty]
    private Profile _profile;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LastMessage))]
    [NotifyPropertyChangedFor(nameof(MessageGroups))]
    private ObservableCollection<Message> _messages;

    [ObservableProperty]
    private bool _isSelected;
}

