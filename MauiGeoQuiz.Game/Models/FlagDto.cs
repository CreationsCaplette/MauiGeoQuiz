using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Game.Models;
public class FlagDto
{
    [JsonPropertyName("png")]
    public string FlagUrl { get; set; } = string.Empty;
}