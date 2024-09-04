using MauiGeoQuiz.Presentation.Services;

namespace MauiGeoQuiz.Services;
public class StringService : IStringService
{
    public string GetString(string key)
    {
        return Resources.Strings.Strings.ResourceManager.GetString(key) ?? "?";
    }
}