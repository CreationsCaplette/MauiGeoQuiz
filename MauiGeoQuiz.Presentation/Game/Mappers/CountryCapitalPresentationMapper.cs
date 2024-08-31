using MauiGeoQuiz.Domain.Game.Models;
using MauiGeoQuiz.Presentation.Game.Models;

namespace MauiGeoQuiz.Presentation.Game.Mappers;
public static class CountryCapitalPresentationMapper
{
    public static CountryCapitalPresentationModel ToCountryCapitalDomain(this CountryCapitalDomainModel countryDomainModel)
    {
        return new CountryCapitalPresentationModel(countryDomainModel.Name, countryDomainModel.Capital);
    }

    public static IEnumerable<CountryCapitalPresentationModel> ToCountryCapitalDomainList(this IEnumerable<CountryCapitalDomainModel> countryDomainModelList)
    {
        return countryDomainModelList.Select(ToCountryCapitalDomain);
    }
}
