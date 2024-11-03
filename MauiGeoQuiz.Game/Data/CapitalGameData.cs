using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.Core.Extensions;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;
using System.Reactive.Linq;

namespace MauiGeoQuiz.Game.Data;

public interface ICapitalGameData : IObservable<CountryCapitalQuestionModel>
{
    void TriggerNextQuestion();
}

public class CapitalGameData(IGameDataRepository updateDataRepository) : ICapitalGameData
{
    private IObserver<CountryCapitalQuestionModel>? _observer;
    private IEnumerable<CountryCapitalModel>? _countryData;

    public IDisposable Subscribe(IObserver<CountryCapitalQuestionModel> observer)
    {
        _observer = observer;

        return Observable
            .FromAsync(updateDataRepository.FetchCountryCapitalData)
            .Do(data => _countryData = data)
            .Select(_ => GetQuestion())
            .Subscribe(observer);
    }

    public void TriggerNextQuestion()
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
        var answer = _countryData
            .Randomize()
            .First();

        var choices = GetChoices(answer, _countryData)
                .Randomize()
                .ToList();

        return new CountryCapitalQuestionModel(
            Question: answer.Name,
            Answers: choices,
            AnswerIndex: choices.IndexOf(answer.Capital));
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
