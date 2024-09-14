using MauiGeoQuiz.Game.Models;

namespace MauiGeoQuiz.Game.Mappers;
public static class CountryRemoteDtoMapper
{
    public static CountryLocalDto ToCountryLocalDto(this CountryRemoteDto countryDto)
    {
        return new CountryLocalDto()
        {
            Name = countryDto.Name.CommonName,
            Capital = countryDto.Capitals.ElementAtOrDefault(0) ?? string.Empty
        };
    }

    public static IEnumerable<CountryLocalDto> ToCountryLocalDtoList(this IEnumerable<CountryRemoteDto> countryDtoList)
    {
        return countryDtoList.Select(ToCountryLocalDto);
    }
}