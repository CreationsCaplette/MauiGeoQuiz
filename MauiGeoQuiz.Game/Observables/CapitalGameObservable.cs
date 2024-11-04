using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.Core.Extensions;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;
using System.Reactive.Linq;

namespace MauiGeoQuiz.Game.Observables;

public interface ICapitalGameObservable : IObservable<CountryCapitalQuestionModel>
{
    void NextQuestion();
}

public class CapitalGameObservable(IGameDataRepository updateDataRepository) : ICapitalGameObservable
{
    private IObserver<CountryCapitalQuestionModel>? _observer;
    private IEnumerable<CountryCapitalModel>? _countryData;

    private List<string>? _answersHistory;

    private int _questionIndex;

    public IDisposable Subscribe(IObserver<CountryCapitalQuestionModel> observer)
    {
        _observer = observer;
        _answersHistory = [];

        return Observable
            .FromAsync(updateDataRepository.FetchCountryCapitalData)
            .Do(data => _countryData = data.ToList())
            .Select(_ => GetQuestion())
            .Subscribe(observer);
    }

    public void NextQuestion()
    {
        if (_countryData?.Count() > 0)
        {
            _observer?.OnNext(GetQuestion());
        }
        else
        {
            _observer?.OnError(new InvalidDataException());
        }
    }

    private CountryCapitalQuestionModel GetQuestion()
    {
        var answer = GetAnswer();
        _answersHistory?.Add(answer.Name);

        var choices = GetChoices(answer, _countryData)
                .Randomize()
                .ToList();

        return new CountryCapitalQuestionModel(
            Question: answer.Name,
            Answers: choices,
            AnswerIndex: choices.IndexOf(answer.Capital));
    }

    private CountryCapitalModel GetAnswer()
    {
        return _countryData
            .Randomize()
            .Where(c => _answersHistory.All(a => a != c.Name))
            .First();
    }

    private IEnumerable<string> GetChoices(CountryCapitalModel answer, IEnumerable<CountryCapitalModel> countryList)
    {
        var answerList = new List<string>(GameConstants.NumberOfChoices)
        {
            answer.Capital,
        };
        answerList.AddRange(countryList
            .Randomize()
            .Take(3)
            .Select(c => c.Capital));
        return answerList;

    }
}
