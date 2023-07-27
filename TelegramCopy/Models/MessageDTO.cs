namespace TelegramCopy.Models;

public class MessageDTO : IDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
}

