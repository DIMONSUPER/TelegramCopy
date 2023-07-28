using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.Models;

public partial class Profile : ObservableObject
{
    public int Id { get; set; }

    public string Image { get; set; }

    public string Nickname { get; set; }

    public DateTime LastOnline { get; set; }
}

