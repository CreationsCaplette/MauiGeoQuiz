namespace MauiGeoQuiz.Core.Architecture;

public interface IUseCase<RESULT>
{
    Task<RESULT> Execute();
}