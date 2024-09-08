using System.Text.Json.Serialization;

namespace MauiGeoQuiz.Game.Models;
public class FlagDataModel
{
    [JsonPropertyName("png")]
    public string FlagUrl { get; set; } = string.Empty;
}