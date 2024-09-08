namespace MauiGeoQuiz.Core.UseCases;

public interface ICommandUseCase<REQUEST>
{
    Task Execute(REQUEST request);
}
