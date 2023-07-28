using System.Collections.ObjectModel;

namespace TelegramCopy.Models;

public class MessageGroup : ObservableCollection<Message>
{
    public MessageGroup(string title, IEnumerable<Message> messages) : base(messages)
    {
        Title = title;
    }

    #region -- Public properties --

    public string Title { get; }

    #endregion
}

