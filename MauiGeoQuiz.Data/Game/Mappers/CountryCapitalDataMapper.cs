using MauiGeoQuiz.Data.Game.Models;
using MauiGeoQuiz.Domain.Game.Models;

namespace MauiGeoQuiz.Data.Game.Mappers;
public static class CountryCapitalDataMapper
{
    public static CountryCapitalDomainModel ToCountryCapitalDomain(this CountryDataModel countryDataModel)
    {
        return new CountryCapitalDomainModel(countryDataModel.Name.CommonName, countryDataModel.Capitals.ElementAt(0) ?? string.Empty);
    }

    public static IEnumerable<CountryCapitalDomainModel> ToCountryCapitalDomainList(this IEnumerable<CountryDataModel> countryDataModelList)
    {
        return countryDataModelList.Select(ToCountryCapitalDomain);
    }
}