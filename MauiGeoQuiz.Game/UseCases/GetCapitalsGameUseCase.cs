using MauiGeoQuiz.Core.UseCases;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;

namespace MauiGeoQuiz.Game.UseCases;
public class GetCapitalsGameUseCase(IUpdateDataRepository updateDataRepository) : IUseCase<IEnumerable<CountryCapitalDomainModel>>
{
    public async Task<IEnumerable<CountryCapitalDomainModel>> Execute()
    {
        return await updateDataRepository.UpdateData();
    }
}