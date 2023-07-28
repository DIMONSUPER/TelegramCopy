using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TelegramCopy.Services;
using TelegramCopy.ViewModels;
using TelegramCopy.Views;

namespace TelegramCopy;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UsePrism(configurePrism => configurePrism
                .RegisterTypes(container => container
                    .RegisterForNavigation<MainPage, MainPageViewModel>())
                .RegisterTypes(container => container
                    .RegisterSingleton<IRepositoryService, RepositoryService>()
                    .RegisterSingleton<IChatService, ChatService>())
                .OnAppStart(nameof(MainPage))
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

