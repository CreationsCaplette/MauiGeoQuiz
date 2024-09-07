namespace MauiGeoQuiz.Domain.Architecture;
public interface ICommandUseCase<REQUEST>
{
    Task Execute(REQUEST request);
}
