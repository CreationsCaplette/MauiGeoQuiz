using MauiGeoQuiz.About;
using MauiGeoQuiz.Data.Game.Datasources;
using MauiGeoQuiz.Data.Game.Repositories;
using MauiGeoQuiz.Domain.About.UseCases;
using MauiGeoQuiz.Domain.Game.Repositories;
using MauiGeoQuiz.Domain.Game.UseCases;
using MauiGeoQuiz.Game;
using MauiGeoQuiz.MainMenu;
using MauiGeoQuiz.Presentation.About;
using MauiGeoQuiz.Presentation.Game.ViewModels;
using MauiGeoQuiz.Presentation.MainMenu.ViewModels;
using MauiGeoQuiz.Presentation.Services;
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
        SetupDomain(builder);
        SetupData(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void SetupServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IStringService, StringService>();
    }

    private static void SetupPresentation(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddScoped<MainMenuViewModel>();

        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddScoped<AboutViewModel>();

        builder.Services.AddTransient<GamePage>();
        builder.Services.AddScoped<GameViewModel>();
    }

    private static void SetupDomain(MauiAppBuilder builder)
    {
        builder.Services.AddScoped<ContactUsUseCase>();
        builder.Services.AddScoped<GetVersionUseCase>();
        builder.Services.AddScoped<GetCapitalsGameUseCase>();
    }

    private static void SetupData(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<HttpClient>();
        builder.Services.AddTransient<ICountriesDataSource, CountriesApiDataSource>();
        builder.Services.AddTransient<IUpdateDataRepository, UpdateDataRepository>();
    }
}
