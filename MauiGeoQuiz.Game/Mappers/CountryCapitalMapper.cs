using MauiGeoQuiz.Game.Models;

namespace MauiGeoQuiz.Game.Mappers;
public static class CountryCapitalMapper
{
    public static CountryCapitalModel ToCountryCapital(this CountryDto countryDto)
    {
        return new CountryCapitalModel(countryDto.Name.CommonName, countryDto.Capitals.ElementAtOrDefault(0) ?? string.Empty);
    }

    public static IEnumerable<CountryCapitalModel> ToCountryCapitalList(this IEnumerable<CountryDto> countryDtoList)
    {
        return countryDtoList.Select(ToCountryCapital);
    }
}