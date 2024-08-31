using MauiGeoQuiz.Domain.Architecture;
using MauiGeoQuiz.Domain.Game.Models;
using MauiGeoQuiz.Domain.Game.Repositories;

namespace MauiGeoQuiz.Domain.Game.UseCases;
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