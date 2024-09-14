namespace MauiGeoQuiz.Game.Datasources;

public interface IScheduledUpdateDatasource
{
    public bool IsUpdateNeeded();
    public void UpdateWasDone();
}
public class ScheduledUpdateDatasource : IScheduledUpdateDatasource
{
    private const string _lastUpdateKey = "LastUpdate";
    private const int _updateFrequencyInDays = 30;
    public bool IsUpdateNeeded()
    {
        var lastUpdate = Preferences.Default.Get(_lastUpdateKey, DateTime.MinValue);
        var test = (DateTime.UtcNow - lastUpdate).Days;
        if (test > _updateFrequencyInDays)
        {
            return true;
        }
        return false;
    }

    public void UpdateWasDone()
    {
        Preferences.Default.Set(_lastUpdateKey, DateTime.UtcNow);
    }
}
