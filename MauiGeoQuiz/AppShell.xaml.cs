using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.ViewPages;

namespace MauiGeoQuiz;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(Pages.MainMenu, typeof(MainPage));
        Routing.RegisterRoute(Pages.About, typeof(AboutPage));
        Routing.RegisterRoute(Pages.Game, typeof(GamePage));
    }
}
