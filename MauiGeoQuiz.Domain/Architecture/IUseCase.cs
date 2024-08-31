namespace MauiGeoQuiz.Domain.Architecture;
public interface IUseCase<RESULT>
{
    Task<RESULT> Execute();
}