using MauiGeoQuiz.Domain.About.Models;
using MauiGeoQuiz.Domain.About.UseCases;
using MauiGeoQuiz.Presentation.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;

namespace MauiGeoQuiz.Presentation.About;
public class AboutViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IStringService _stringService;
    private readonly GetVersionUseCase _getVersionUseCase;
    private readonly ContactUsUseCase _contactUsUseCase;

    [Reactive] public string Version { get; private set; }
    public ReactiveCommand<Unit, Unit> ContactUsCommand { get; }

    public AboutViewModel(IStringService stringService, GetVersionUseCase getVersionUseCase, ContactUsUseCase contactUsUseCase)
    {
        _stringService = stringService;
        _getVersionUseCase = getVersionUseCase;
        _contactUsUseCase = contactUsUseCase;

        ContactUsCommand = ReactiveCommand.Create(OnContactUs);

        Version = $"{_stringService.GetString("Version")}: {_getVersionUseCase.Execute()}";
    }

    public ViewModelActivator Activator { get; } = new();

    private async void OnContactUs()
    {
        var contactUs = new ContactUsModel()
        {
            AppName = _stringService.GetString("AppName"),
            AppVersion = _getVersionUseCase.Execute(),
            EmailAddress = _stringService.GetString("EmailCreator")
        };
        await _contactUsUseCase.Execute(contactUs);
    }
}
