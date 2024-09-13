using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Game.Models;
public class CountryNamesDto
{
    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;
}
