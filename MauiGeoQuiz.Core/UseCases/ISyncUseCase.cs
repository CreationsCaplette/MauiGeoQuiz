namespace MauiGeoQuiz.Core.Architecture;

public interface ISyncUseCase<RESULT>
{
    RESULT Execute();
}