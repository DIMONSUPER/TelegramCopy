namespace TelegramCopy.Helpers;

public static class DateTimeHelper
{
    public static string CovertToNormalizedDate(DateTime dateTime)
    {
        string result;

        if (DateTime.Today == dateTime.Date)
        {
            result = nameof(DateTime.Today);
        }
        else
        {
            result = dateTime.ToString("dd MMMM");
        }

        return result;
    }
}

