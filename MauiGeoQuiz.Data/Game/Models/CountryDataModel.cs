using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Data.Game.Models;
public class CountryDataModel
{
    [JsonPropertyName("name")]
    public CountryNamesDataModel Name { get; set; } = new CountryNamesDataModel();

    [JsonPropertyName("capital")]
    public List<string> Capitals { get; set; } = new List<string>();

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("flags")]
    public FlagDataModel Flag { get; set; } = new FlagDataModel();
}