namespace MauiGeoQuiz.Core.Extensions;
public static class ListExtensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> list)
    {
        var random = new Random();
        return list.OrderBy(x => random.Next());
    }
}
