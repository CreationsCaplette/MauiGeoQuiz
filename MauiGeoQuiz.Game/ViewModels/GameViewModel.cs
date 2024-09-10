using MauiGeoQuiz.Game.UseCases;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;

namespace MauiGeoQuiz.Game.ViewModels;
public class GameViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly INavigationService _navigationService;
    private readonly GetCapitalsGameUseCase _getCapitalsGameUseCase;
    private IEnumerable<CountryCapitalModel>? _countryCapitalsList;
    private int _questionIndex;

    [Reactive] public string? QuizProgress { get; set; }
    [Reactive] public string? Question { get; set; }
    [Reactive] public string? AnswerOne { get; set; }
    [Reactive] public string? AnswerTwo { get; set; }
    [Reactive] public string? AnswerThree { get; set; }
    [Reactive] public string? AnswerFour { get; set; }

    public ReactiveCommand<Unit, Unit> AnswerOneCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerTwoCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerThreeCommand { get; }
    public ReactiveCommand<Unit, Unit> AnswerFourCommand { get; }
    public ReactiveCommand<Unit, Unit> NextQuestionCommand { get; }

    public ViewModelActivator Activator { get; } = new();

    public GameViewModel(INavigationService navigationService, GetCapitalsGameUseCase getCapitalsGameUseCase)
    {
        _navigationService = navigationService;
        _getCapitalsGameUseCase = getCapitalsGameUseCase;

        AnswerOneCommand = ReactiveCommand.Create(OnAnswerOne);
        AnswerTwoCommand = ReactiveCommand.Create(OnAnswerTwo);
        AnswerThreeCommand = ReactiveCommand.Create(OnAnswerThree);
        AnswerFourCommand = ReactiveCommand.Create(OnAnswerFour);
        NextQuestionCommand = ReactiveCommand.Create(OnNextQuestion);
    }

    public async Task GetQuizData()
    {
        _questionIndex = -1;
        _countryCapitalsList = await _getCapitalsGameUseCase.Execute();

        DisplayNextQuestion();
    }

    private async void OnAnswerOne()
    {

    }

    private async void OnAnswerTwo()
    {

    }

    private async void OnAnswerThree()
    {

    }

    private async void OnAnswerFour()
    {

    }

    private void OnNextQuestion()
    {
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
            AnswerTwo = _countryCapitalsList.ElementAt(_questionIndex).Capital;
            AnswerThree = _countryCapitalsList.ElementAt(_questionIndex).Capital;
            AnswerFour = _countryCapitalsList.ElementAt(_questionIndex).Capital;
        }
    }
}
