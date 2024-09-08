using MauiGeoQuiz.Core.Architecture;
using MauiGeoQuiz.Game.Models;
using MauiGeoQuiz.Game.Repositories;

namespace MauiGeoQuiz.Game.UseCases;
public class GetCapitalsGameUseCase : IUseCase<IEnumerable<CountryCapitalDomainModel>>
{
    private readonly IUpdateDataRepository _updateDataRepository;

    public GetCapitalsGameUseCase(IUpdateDataRepository updateDataRepository)
    {
        _updateDataRepository = updateDataRepository;
    }

    public async Task<IEnumerable<CountryCapitalDomainModel>> Execute()
    {
        return await _updateDataRepository.UpdateData();
    }
}