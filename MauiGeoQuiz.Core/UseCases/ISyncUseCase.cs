namespace MauiGeoQuiz.Domain.Architecture;
public interface ISyncUseCase<RESULT>
{
    RESULT Execute();
}