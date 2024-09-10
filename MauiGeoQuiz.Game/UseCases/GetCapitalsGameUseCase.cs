using MauiGeoQuiz.Core.Constants;
using MauiGeoQuiz.Core.Extensions;
using MauiGeoQuiz.Core.UseCases;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;

namespace MauiGeoQuiz.Game.UseCases;
public class GetCapitalsGameUseCase(IUpdateDataRepository updateDataRepository) : IUseCase<IEnumerable<CountryCapitalModel>>
{
    public async Task<IEnumerable<CountryCapitalModel>> Execute()
    {
        var fullCountryList = await updateDataRepository.FetchCountryCapitalData();

        var questionsList = new List<CountryCapitalModel>(GameConstants.NumberOfQuestions);
        for (var i = 0; i < GameConstants.NumberOfQuestions; i++)
        {
            questionsList.Add(fullCountryList.Randomize().First());
        }

        return questionsList;
    }
}