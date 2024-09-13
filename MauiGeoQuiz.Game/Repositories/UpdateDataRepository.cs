using MauiGeoQuiz.Game.Mappers;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Datasources;

namespace MauiGeoQuiz.Game.Repositories;

public interface IUpdateDataRepository
{
    Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData();
}
public class UpdateDataRepository(ICountriesRemoteDatasource countriesRemoteDatasource) : IUpdateDataRepository
{
    public async Task<IEnumerable<CountryCapitalModel>> FetchCountryCapitalData()
    {
        var countriesList = await countriesRemoteDatasource.FetchCountriesData();
        return countriesList.Where(c => c.Capitals.Count() > 0).ToCountryCapitalList();
    }
}
