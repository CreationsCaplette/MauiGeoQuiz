using MauiGeoQuiz.Data.Game.Datasources;
using MauiGeoQuiz.Data.Game.Mappers;
using MauiGeoQuiz.Domain.Game.Models;
using MauiGeoQuiz.Domain.Game.Repositories;

namespace MauiGeoQuiz.Data.Game.Repositories;
public class UpdateDataRepository(ICountriesDataSource countriesDataSource) : IUpdateDataRepository
{
    public async Task<IEnumerable<CountryCapitalDomainModel>> UpdateData()
    {
        return (await countriesDataSource.FetchCountriesData()).Take(10).ToCountryCapitalDomainList();
    }
}
