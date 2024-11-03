using System.Reactive.Linq;

namespace MauiGeoQuiz.Core.Services;

public interface ICountdownTimer
{
    IObservable<float> GetTimerObservable(long timerMilliseconds);
}
public class CountdownTimer : ICountdownTimer
{
    private const int TickTimeSpan = 100;

    public IObservable<float> GetTimerObservable(long timerMilliseconds)
    {
        var totalTicks = timerMilliseconds / TickTimeSpan;

        return Observable
            .Timer(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(TickTimeSpan))
            .TakeWhile(tick => tick <= totalTicks)
            .Select(tick => (timerMilliseconds - (tick * TickTimeSpan)) / 1000.0f);
    }
}
