using MauiGeoQuiz.Presentation.Constants;
using MauiGeoQuiz.Presentation.Services;
using ReactiveUI;
using System.Reactive;

namespace MauiGeoQuiz.Presentation.MainMenu;

public class MainMenuViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly INavigationService _navigationService;

    public ReactiveCommand<Unit, Unit> CapitalsCommand { get; }
    public ReactiveCommand<Unit, Unit> AboutCommand { get; }
    public ReactiveCommand<Unit, Unit> PrivacyCommand { get; }

    public MainMenuViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        CapitalsCommand = ReactiveCommand.Create(OnCapitals);
        AboutCommand = ReactiveCommand.Create(OnAbout);
        PrivacyCommand = ReactiveCommand.Create(OnPrivacy);
    }

    public ViewModelActivator Activator { get; } = new();

    private async void OnCapitals()
    {
        await _navigationService.NavigateToPage(Pages.Game);
    }

    private async void OnAbout()
    {
        await _navigationService.NavigateToPage(Pages.About);
    }

    private void OnPrivacy()
    {

    }
}
