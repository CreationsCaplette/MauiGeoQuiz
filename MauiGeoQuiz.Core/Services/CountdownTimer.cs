using System.Reactive.Linq;

namespace MauiGeoQuiz.Core.Services;

public interface ICountdownTimer
{
    IDisposable StartCountdown(long timerMilliseconds, Action<float> onUpdateAction, Action onCompleteAction);
}
public class CountdownTimer : ICountdownTimer
{
    private const int TickTimeSpan = 50;

    public IDisposable StartCountdown(long timerMilliseconds, Action<float> onUpdateAction, Action onCompleteAction)
    {
        var totalTicks = timerMilliseconds / TickTimeSpan;

        return Observable
            .Timer(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(TickTimeSpan))
            .TakeWhile(tick => tick <= totalTicks)
            .Select(tick => (timerMilliseconds - (tick * TickTimeSpan)) / 1000.0f)
            .Subscribe(onUpdateAction, onCompleteAction);
    }
}
