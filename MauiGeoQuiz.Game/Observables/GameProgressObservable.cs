using MauiGeoQuiz.Core.Constants;
using System.Reactive.Linq;

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

        return Observable
            .Return(GetProgressString())
            .Subscribe(_observer);
    }

    public void Advance()
    {
        if (_progressIndex < GameConstants.NumberOfQuestions)
        {
            _observer?.OnNext(GetProgressString());
        }
        else
        {
            _observer?.OnCompleted();
        }
    }

    private string GetProgressString() => $"{++_progressIndex}/{GameConstants.NumberOfQuestions}";
}
