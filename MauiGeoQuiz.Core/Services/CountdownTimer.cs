using System.Reactive.Linq;

namespace MauiGeoQuiz.Core.Services;

public interface ICountdownTimer
{
    IObservable<float> GetTimerObservable(long timerMilliseconds, long tickTimeSpan);
}
public class CountdownTimer : ICountdownTimer
{
    public IObservable<float> GetTimerObservable(long timerMilliseconds, long tickTimeSpan)
    {
        var totalTicks = timerMilliseconds / tickTimeSpan;

        return Observable
            .Timer(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(tickTimeSpan))
            .TakeWhile(tick => tick <= totalTicks)
            .Select(tick => (timerMilliseconds - (tick * tickTimeSpan)) / 1000.0f);
    }
}
