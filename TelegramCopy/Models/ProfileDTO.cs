using SQLite;

namespace TelegramCopy.Models;

public class ProfileDTO : IDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Image { get; set; }
    public string Nickname { get; set; }
    public DateTime LastOnline { get; set; }
}

