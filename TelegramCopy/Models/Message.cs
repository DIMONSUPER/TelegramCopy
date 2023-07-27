using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.Models;

public class Message : ObservableObject
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime SendDate { get; set; }
}

