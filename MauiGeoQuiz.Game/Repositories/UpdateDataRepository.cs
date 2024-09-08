using MauiGeoQuiz.Game.Mappers;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Datasources;

namespace MauiGeoQuiz.Game.Repositories;

public interface IUpdateDataRepository
{
    Task<IEnumerable<CountryCapitalDomainModel>> UpdateData();
}
public class UpdateDataRepository(ICountriesDatasource countriesDataSource) : IUpdateDataRepository
{
    public async Task<IEnumerable<CountryCapitalDomainModel>> UpdateData()
    {
        return (await countriesDataSource.FetchCountriesData()).Take(10).ToCountryCapitalDomainList();
    }
}
