using MauiGeoQuiz.Game.Models;

namespace MauiGeoQuiz.Game.Mappers;
public static class CountryLocalDtoMapper
{
    public static CountryCapitalModel ToCountryCapital(this CountryLocalDto countryDto)
    {
        return new CountryCapitalModel(countryDto.Name, countryDto.Capital);
    }

    public static IEnumerable<CountryCapitalModel> ToCountryCapitalList(this IEnumerable<CountryLocalDto> countryDtoList)
    {
        return countryDtoList.Select(ToCountryCapital);
    }
}