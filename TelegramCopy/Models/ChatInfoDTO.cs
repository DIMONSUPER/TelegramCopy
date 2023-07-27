namespace TelegramCopy.Models;

public class ChatInfoDTO : IDTO
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public int LastMessageId { get; set; }
}

