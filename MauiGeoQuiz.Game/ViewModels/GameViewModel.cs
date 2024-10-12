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
    [Reactive] public Validity AnswerOneValidity { get; set; }
    [Reactive] public Validity AnswerTwoValidity { get; set; }
    [Reactive] public Validity AnswerThreeValidity { get; set; }
    [Reactive] public Validity AnswerFourValidity { get; set; }
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
        var answerValidity = currentQuestion.AnswerIndex == guessIndex ? Validity.Valid : Validity.Invalid;

        AnswersEnabled = false;

        switch (guessIndex)
        {
            case 0:
                AnswerOneValidity = answerValidity;
                break;
            case 1:
                AnswerTwoValidity = answerValidity;
                break;
            case 2:
                AnswerThreeValidity = answerValidity;
                break;
            case 3:
                AnswerFourValidity = answerValidity;
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
            AnswerOneValidity = Validity.Idle;
            AnswerTwo = currentQuestion.Answers.ElementAt(1);
            AnswerTwoValidity = Validity.Idle;
            AnswerThree = currentQuestion.Answers.ElementAt(2);
            AnswerThreeValidity = Validity.Idle;
            AnswerFour = currentQuestion.Answers.ElementAt(3);
            AnswerFourValidity = Validity.Idle;

            AnswersEnabled = true;
            NextQuestionVisible = false;
        }
    }
}
