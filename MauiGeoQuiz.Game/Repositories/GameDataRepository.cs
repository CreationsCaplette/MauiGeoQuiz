using MauiGeoQuiz.Game.Mappers;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Datasources;

namespace MauiGeoQuiz.Game.Repositories;

public interface IGameDataRepository
{
    Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData();
}

public class GameDataRepository(
    IScheduledUpdateDatasource scheduledUpdateDatasource,
    ICountriesRemoteDatasource countriesRemoteDatasource,
    ICountriesLocalDatasource countriesLocalDatasource) : IGameDataRepository
{
    public async Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData()
    {
        if (scheduledUpdateDatasource.IsUpdateNeeded())
        {
            await PerformCacheUpdate();
        }

        return (await countriesLocalDatasource.GetAllCountries())
            .ToCountryCapitalList();
    }

    private async Task PerformCacheUpdate()
    {
        var countriesList = (await countriesRemoteDatasource.FetchCountriesData())
            .Where(c => c.Capitals.Count() > 0)
            .ToCountryLocalDtoList();

        foreach (var country in countriesList)
        {
            await countriesLocalDatasource.InsertOrUpdateCountry(country);
        }

        scheduledUpdateDatasource.UpdateWasDone();
    }
}
