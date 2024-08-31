using MauiGeoQuiz.Game;
using MauiGeoQuiz.MainMenu;
using MauiGeoQuiz.Presentation.Constants;

namespace MauiGeoQuiz;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(Pages.MainMenu, typeof(MainPage));
        Routing.RegisterRoute(Pages.Game, typeof(GamePage));
    }
}
