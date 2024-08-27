using MauiGeoQuiz.Domain.Game.UseCases;
using MauiGeoQuiz.Presentation.Game.Mappers;
using MauiGeoQuiz.Presentation.Game.Models;
using MauiGeoQuiz.Presentation.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;

namespace MauiGeoQuiz.Presentation.Game.ViewModels;
public class GameViewModel(
    INavigationService navigationService,
    GetCapitalsGameUseCase getCapitalsGameUseCase
    ) : ReactiveObject, IActivatableViewModel
{
    private IEnumerable<CountryCapitalPresentationModel> _countryCapitalsList;
    private int _questionIndex;

    [Reactive] public string QuizProgress { get; set; }
    [Reactive] public string Question { get; set; }
    [Reactive] public string AnswerOne { get; set; }

    public ReactiveCommand<Unit, Unit> AnswerOneCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerTwoCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerThreeCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerFourCommand { get; }
    public ReactiveCommand<Unit, Unit> NextQuestionCommand { get; }

    public ViewModelActivator Activator { get; } = new();

    public async Task GetQuizData()
    {
        _questionIndex = -1;
        _countryCapitalsList = (await getCapitalsGameUseCase.Execute()).ToCountryCapitalDomainList();

        DisplayNextQuestion();
    }

    private void DisplayNextQuestion()
    {
        _questionIndex++;
        if (_questionIndex < _countryCapitalsList.Count())
        {
            QuizProgress = $"{_questionIndex + 1}/{_countryCapitalsList.Count()}";
            Question = _countryCapitalsList.ElementAt(_questionIndex).Name;
            AnswerOne = _countryCapitalsList.ElementAt(_questionIndex).Capital;
        }
    }
}
