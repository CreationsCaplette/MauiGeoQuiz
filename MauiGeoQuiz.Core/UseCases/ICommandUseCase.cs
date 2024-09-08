namespace MauiGeoQuiz.Core.Architecture;

public interface ICommandUseCase<REQUEST>
{
    Task Execute(REQUEST request);
}
