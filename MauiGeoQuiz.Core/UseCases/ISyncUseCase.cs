namespace MauiGeoQuiz.Core.UseCases;

public interface ISyncUseCase<RESULT>
{
    RESULT Execute();
}