using MauiGeoQuiz.Game.Mappers;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Datasources;

namespace MauiGeoQuiz.Game.Repositories;

public interface IUpdateDataRepository
{
    Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData();
}
public class UpdateDataRepository(
    ICountriesRemoteDatasource countriesRemoteDatasource,
    ICountriesLocalDatasource countriesLocalDatasource) : IUpdateDataRepository
{
    public async Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData()
    {
        var countriesList = (await countriesRemoteDatasource.FetchCountriesData())
            .Where(c => c.Capitals.Count() > 0)
            .ToCountryLocalDtoList();

        foreach (var country in countriesList)
        {
            await countriesLocalDatasource.InsertOrUpdateCountry(country);
        }

        return (await countriesLocalDatasource.GetAllCountries())
            .ToCountryCapitalList();
    }
}
