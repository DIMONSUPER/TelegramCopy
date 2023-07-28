using System.Collections.ObjectModel;

namespace TelegramCopy.Models;

public class MessageGroup : ObservableCollection<Message>
{
    public MessageGroup(DateTime title, IEnumerable<Message> messages) : base(messages)
    {
        Title = title;
    }

    #region -- Public properties --

    public DateTime Title { get; }

    #endregion
}

