using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.Models;

public partial class Message : ObservableObject
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public string Content { get; set; }

    public DateTime SendDate { get; set; }
}

