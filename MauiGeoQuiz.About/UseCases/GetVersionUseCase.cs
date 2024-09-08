using MauiGeoQuiz.Core.Architecture;

namespace MauiGeoQuiz.About.UseCases;
public class GetVersionUseCase : ISyncUseCase<string>
{
    public string Execute()
    {
        var version = AppInfo.Current.Version;
        return $"{version.Major}.{version.Minor}";
    }
}
