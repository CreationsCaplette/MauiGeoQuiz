namespace MauiGeoQuiz.Core.UseCases;

public interface IUseCase<RESULT>
{
    Task<RESULT> Execute();
}