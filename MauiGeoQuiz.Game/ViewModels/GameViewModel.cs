using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Reactive.Linq;
using MauiGeoQuiz.Core.Enums;
using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.Game.Data;

namespace MauiGeoQuiz.Game.ViewModels;
public class GameViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly INavigationService _navigationService;
    private readonly ICapitalGameData _capitalGameData;
    private readonly ICountdownTimer _countdownTimer;

    private int _questionIndex;
    private int _answerIndex;

    private IDisposable? _gameDataSubscription;
    private IDisposable? _timerSubscription;

    [Reactive] public int Score { get; set; } = 0;
    [Reactive] public string Progress { get; set; } = string.Empty;
    [Reactive] public float Timer { get; set; } = 0;
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

    public GameViewModel(INavigationService navigationService, ICapitalGameData capitalGameData, ICountdownTimer countdownTimer)
    {
        _navigationService = navigationService;
        _capitalGameData = capitalGameData;
        _countdownTimer = countdownTimer;

        AnswerOneCommand = ReactiveCommand.Create(() => ValidateAnswer(0));
        AnswerTwoCommand = ReactiveCommand.Create(() => ValidateAnswer(1));
        AnswerThreeCommand = ReactiveCommand.Create(() => ValidateAnswer(2));
        AnswerFourCommand = ReactiveCommand.Create(() => ValidateAnswer(3));
        NextQuestionCommand = ReactiveCommand.Create(OnNextQuestion);

        _gameDataSubscription = _capitalGameData
            .Subscribe(
            DisplayQuestion,
            () => { });
    }

    private void ValidateAnswer(int guessIndex)
    {
        _timerSubscription?.Dispose();

        AnswersEnabled = false;
        AnswerOneState = GameButtonStates.Disabled;
        AnswerTwoState = GameButtonStates.Disabled;
        AnswerThreeState = GameButtonStates.Disabled;
        AnswerFourState = GameButtonStates.Disabled;

        var isAnswerGood = guessIndex == _answerIndex;

        var answerState = isAnswerGood ? GameButtonStates.Positive : GameButtonStates.Negative;
        Score += isAnswerGood ? (int)(Timer * 10) : 0;

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
        _capitalGameData.TriggerNextQuestion();
    }

    private void DisplayQuestion(CountryCapitalQuestionModel question)
    {
        _questionIndex++;

        Progress = $"{_questionIndex + 1}/{GameConstants.NumberOfQuestions}";

        Question = question.Question;
        AnswerOne = question.Answers.ElementAt(0);
        AnswerTwo = question.Answers.ElementAt(1);
        AnswerThree = question.Answers.ElementAt(2);
        AnswerFour = question.Answers.ElementAt(3);
        _answerIndex = question.AnswerIndex;

        AnswerOneState = GameButtonStates.Idle;
        AnswerTwoState = GameButtonStates.Idle;
        AnswerThreeState = GameButtonStates.Idle;
        AnswerFourState = GameButtonStates.Idle;

        AnswersEnabled = true;
        NextQuestionVisible = false;

        _timerSubscription = _countdownTimer
            .GetTimerObservable(GameConstants.TimerMilliseconds)
            .Subscribe(UpdateTimer, OnCountdownFinished);
    }

    private void UpdateTimer(float currentSecond)
    {
        Timer = currentSecond;
    }

    private void OnCountdownFinished()
    {
        _timerSubscription?.Dispose();

        AnswersEnabled = false;
        AnswerOneState = GameButtonStates.Disabled;
        AnswerTwoState = GameButtonStates.Disabled;
        AnswerThreeState = GameButtonStates.Disabled;
        AnswerFourState = GameButtonStates.Disabled;

        switch (_answerIndex)
        {
            case 0:
                AnswerOneState = GameButtonStates.Negative;
                break;
            case 1:
                AnswerTwoState = GameButtonStates.Negative;
                break;
            case 2:
                AnswerThreeState = GameButtonStates.Negative;
                break;
            case 3:
                AnswerFourState = GameButtonStates.Negative;
                break;
        }

        NextQuestionVisible = true;
    }
}
