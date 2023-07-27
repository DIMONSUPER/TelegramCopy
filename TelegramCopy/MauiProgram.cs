using Microsoft.Extensions.Logging;
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
            .UsePrism(configurePrism => configurePrism
                .RegisterTypes(container => container
                    .RegisterForNavigation<MainPage, MainPageViewModel>())
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

