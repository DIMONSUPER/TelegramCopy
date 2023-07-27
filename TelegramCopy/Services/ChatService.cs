namespace TelegramCopy.Services;

public class ChatService : IChatService
{
    private readonly IRepositoryService _repositoryService;

    public ChatService(
        IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }
}

