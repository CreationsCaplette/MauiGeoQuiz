using DynamicData;
using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.Core.Extensions;
using MauiGeoQuiz.Core.UseCases;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;

namespace MauiGeoQuiz.Game.UseCases;
public class GetCapitalsGameUseCase(IUpdateDataRepository updateDataRepository) : IUseCase<IEnumerable<CountryCapitalQuestionModel>>
{
    public async Task<IEnumerable<CountryCapitalQuestionModel>> Execute()
    {
        var fullCountryList = await updateDataRepository.FetchCountryCapitalData();

        var questionsList = new List<CountryCapitalQuestionModel>(GameConstants.NumberOfQuestions);
        for (var i = 0; i < GameConstants.NumberOfQuestions; i++)
        {
            var answer = fullCountryList.Randomize().First();
            var choices = GetChoices(answer, fullCountryList).Randomize().ToList();
            var question = new CountryCapitalQuestionModel(
                Question: answer.Name,
                Answers: choices,
                AnswerIndex: GetAnswerIndex(answer.Capital, choices));
            questionsList.Add(question);
        }

        return questionsList;
    }

    private IEnumerable<string> GetChoices(CountryCapitalModel answer, IEnumerable<CountryCapitalModel> countryList)
    {
        var answerList = new List<string>(GameConstants.NumberOfChoices)
        {
            answer.Capital,
        };
        answerList.AddRange(countryList.Randomize().Take(3).Select(c => c.Capital));
        return answerList;

    }

    private int GetAnswerIndex(string answer, IEnumerable<string> choices)
    {
        return choices.IndexOf(answer);
    }
}