using SQLite;

namespace TelegramCopy.Models;

public class ChatInfoDTO : IDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int SenderProfileId { get; set; }
}

