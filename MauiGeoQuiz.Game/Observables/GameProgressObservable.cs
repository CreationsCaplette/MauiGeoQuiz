using MauiGeoQuiz.Core.Constants;
using System.Reactive.Disposables;

namespace MauiGeoQuiz.Game.Observables;

public interface IGameProgressObservable : IObservable<string>
{
    void Advance();
}

public class GameProgressObservable() : IGameProgressObservable
{
    private IObserver<string>? _observer;
    private int _progressIndex;

    public IDisposable Subscribe(IObserver<string> observer)
    {
        _observer = observer;
        _progressIndex = 0;

        Advance();

        return Disposable.Empty;
    }

    public void Advance()
    {
        if (_progressIndex < GameConstants.NumberOfQuestions)
        {
            _observer?.OnNext($"{++_progressIndex}/{GameConstants.NumberOfQuestions}");
        }
        else
        {
            _observer?.OnCompleted();
        }
    }
}
