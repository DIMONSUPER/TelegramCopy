using SQLite;

namespace TelegramCopy.Models;

public class MessageDTO : IDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ChatId { get; set; }
    public bool IsMyMessage { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}

