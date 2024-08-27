using MauiGeoQuiz.Services;
using Microsoft.Extensions.Logging;

namespace MauiGeoQuiz;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        SetupServices(builder);
        SetupPresentation(builder);

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void SetupServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
    }

    private static void SetupPresentation(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddScoped<MainMenuViewModel>();
    }
}
