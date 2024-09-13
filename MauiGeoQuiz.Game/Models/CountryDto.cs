using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Game.Models;
public class CountryDto
{
    [JsonPropertyName("name")]
    public CountryNamesDto Name { get; set; } = new CountryNamesDto();

    [JsonPropertyName("capital")]
    public IEnumerable<string> Capitals { get; set; } = [];

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("flags")]
    public FlagDto Flag { get; set; } = new FlagDto();
}