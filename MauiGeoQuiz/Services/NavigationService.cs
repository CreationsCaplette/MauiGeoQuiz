namespace MauiGeoQuiz.Services;
public class NavigationService : INavigationService
{
    public async Task NavigateToPage(string page)
    {
        await Shell.Current.GoToAsync(page, true);
    }
}
