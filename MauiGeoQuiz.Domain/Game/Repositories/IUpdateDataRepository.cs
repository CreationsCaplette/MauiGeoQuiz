using MauiGeoQuiz.Domain.Game.Models;

namespace MauiGeoQuiz.Domain.Game.Repositories;
public interface IUpdateDataRepository
{
    Task<IEnumerable<CountryCapitalDomainModel>> UpdateData();
}
