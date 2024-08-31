using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Data.Game.Models;
public class CountryNamesDataModel
{
    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;
}
