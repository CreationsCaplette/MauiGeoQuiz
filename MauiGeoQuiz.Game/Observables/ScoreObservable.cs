using System.Reactive.Linq;

namespace MauiGeoQuiz.Game.Observables;

public interface IScoreObservable : IObservable<int>
{
    void ProcessScore(bool valid, float timer);
}

public class ScoreObservable() : IScoreObservable
{
    private IObserver<int>? _observer;
    private int _score;

    public IDisposable Subscribe(IObserver<int> observer)
    {
        _observer = observer;
        _score = 0;

        return Observable
            .Return(_score)
            .Subscribe(_observer);
    }

    public void ProcessScore(bool valid, float timer)
    {
        _score += valid ? (int)(timer * 10) : 0;
        _observer?.OnNext(_score);
    }
}
