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
    private IEnumerable<CountryCapitalQuestionModel>? _countryCapitalQuestions;
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
        _countryCapitalQuestions = await _getCapitalsGameUseCase.Execute();

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
        if (_questionIndex < _countryCapitalQuestions?.Count())
        {
            QuizProgress = $"{_questionIndex + 1}/{_countryCapitalQuestions.Count()}";
            Question = _countryCapitalQuestions.ElementAt(_questionIndex).Question;
            AnswerOne = _countryCapitalQuestions.ElementAt(_questionIndex).Answers.ElementAt(0);
            AnswerTwo = _countryCapitalQuestions.ElementAt(_questionIndex).Answers.ElementAt(1);
            AnswerThree = _countryCapitalQuestions.ElementAt(_questionIndex).Answers.ElementAt(2);
            AnswerFour = _countryCapitalQuestions.ElementAt(_questionIndex).Answers.ElementAt(3);
        }
    }
}
