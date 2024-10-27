using MauiGeoQuiz.Game.UseCases;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using MauiGeoQuiz.Core.Enums;

namespace MauiGeoQuiz.Game.ViewModels;
public class GameViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly INavigationService _navigationService;
    private readonly GetCapitalsGameUseCase _getCapitalsGameUseCase;
    private IEnumerable<CountryCapitalQuestionModel> _countryCapitalQuestions = [];
    private int _questionIndex;

    [Reactive] public string QuizProgress { get; set; } = string.Empty;
    [Reactive] public string Question { get; set; } = string.Empty;
    [Reactive] public string AnswerOne { get; set; } = string.Empty;
    [Reactive] public string AnswerTwo { get; set; } = string.Empty;
    [Reactive] public string AnswerThree { get; set; } = string.Empty;
    [Reactive] public string AnswerFour { get; set; } = string.Empty;
    [Reactive] public GameButtonStates AnswerOneState { get; set; }
    [Reactive] public GameButtonStates AnswerTwoState { get; set; }
    [Reactive] public GameButtonStates AnswerThreeState { get; set; }
    [Reactive] public GameButtonStates AnswerFourState { get; set; }
    [Reactive] public bool AnswersEnabled { get; set; }
    [Reactive] public bool NextQuestionVisible { get; set; }

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

        AnswerOneCommand = ReactiveCommand.Create(() => ValidateAnswer(0));
        AnswerTwoCommand = ReactiveCommand.Create(() => ValidateAnswer(1));
        AnswerThreeCommand = ReactiveCommand.Create(() => ValidateAnswer(2));
        AnswerFourCommand = ReactiveCommand.Create(() => ValidateAnswer(3));
        NextQuestionCommand = ReactiveCommand.Create(OnNextQuestion);
    }

    public async Task GetQuizData()
    {
        _questionIndex = -1;
        _countryCapitalQuestions = await _getCapitalsGameUseCase.Execute();

        DisplayNextQuestion();
    }

    private void ValidateAnswer(int guessIndex)
    {
        var currentQuestion = _countryCapitalQuestions.ElementAt(_questionIndex);
        var answerState = currentQuestion.AnswerIndex == guessIndex ? GameButtonStates.Positive : GameButtonStates.Negative;

        AnswersEnabled = false;
        AnswerOneState = GameButtonStates.Disabled;
        AnswerTwoState = GameButtonStates.Disabled;
        AnswerThreeState = GameButtonStates.Disabled;
        AnswerFourState = GameButtonStates.Disabled;

        switch (guessIndex)
        {
            case 0:
                AnswerOneState = answerState;
                break;
            case 1:
                AnswerTwoState = answerState;
                break;
            case 2:
                AnswerThreeState = answerState;
                break;
            case 3:
                AnswerFourState = answerState;
                break;
        }

        NextQuestionVisible = true;
    }

    private void OnNextQuestion()
    {
        DisplayNextQuestion();
    }

    private void DisplayNextQuestion()
    {
        _questionIndex++;
        if (_questionIndex < _countryCapitalQuestions.Count())
        {
            QuizProgress = $"{_questionIndex + 1}/{_countryCapitalQuestions.Count()}";

            var currentQuestion = _countryCapitalQuestions.ElementAt(_questionIndex);
            Question = currentQuestion.Question;
            AnswerOne = currentQuestion.Answers.ElementAt(0);
            AnswerOneState = GameButtonStates.Idle;
            AnswerTwo = currentQuestion.Answers.ElementAt(1);
            AnswerTwoState = GameButtonStates.Idle;
            AnswerThree = currentQuestion.Answers.ElementAt(2);
            AnswerThreeState = GameButtonStates.Idle;
            AnswerFour = currentQuestion.Answers.ElementAt(3);
            AnswerFourState = GameButtonStates.Idle;

            AnswersEnabled = true;
            NextQuestionVisible = false;
        }
    }
}
