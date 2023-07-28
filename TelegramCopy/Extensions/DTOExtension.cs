using TelegramCopy.Models;

namespace TelegramCopy.Extensions;

public static class DTOExtension
{
    public static ProfileDTO ToDTO(this Profile profile)
    {
        return new()
        {
            Id = profile.Id,
            Image = profile.Image,
            LastOnline = profile.LastOnline,
            Nickname = profile.Nickname,
        };
    }

    public static Profile ToProfile(this ProfileDTO profile)
    {
        return new()
        {
            Id = profile.Id,
            Image = profile.Image,
            LastOnline = profile.LastOnline,
            Nickname = profile.Nickname,
        };
    }

    public static ChatInfoDTO ToDTO(this ChatInfo chat)
    {
        return new()
        {
            Id = chat.Id,
            SenderProfileId = chat.Profile.Id,
        };
    }

    public static ChatInfo ToChatInfo(this ChatInfoDTO chat, Profile profile, IEnumerable<Message> messages)
    {
        return new()
        {
            Id = chat.Id,
            Profile = profile,
            Messages = new(messages),
        };
    }

    public static MessageDTO ToDTO(this Message message)
    {
        return new()
        {
            Id = message.Id,
            SendDate = message.SendDate,
            Content = message.Content,
            ChatId = message.ChatId,
        };
    }

    public static Message ToMessage(this MessageDTO message)
    {
        return new()
        {
            Id = message.Id,
            SendDate = message.SendDate,
            Content = message.Content,
            ChatId = message.ChatId,
        };
    }
}

