using MauiGeoQuiz.Game.Models;
using SQLite;

namespace MauiGeoQuiz.Game.Datasources;

public interface ICountriesLocalDatasource
{
    public Task<int> InsertOrUpdateCountry(CountryLocalDto countryLocalDto);
    public Task<IEnumerable<CountryLocalDto>> GetAllCountries();
}
public class CountriesLocalDatasource : ICountriesLocalDatasource
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection _connection;

    public CountriesLocalDatasource()
    {
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "geoquiz.db");
        _connection = new SQLiteAsyncConnection(_dbPath);
    }

    public async Task<int> InsertOrUpdateCountry(CountryLocalDto countryLocalDto)
    {
        await _connection.CreateTableAsync<CountryLocalDto>();
        if (countryLocalDto.Id != 0)
            await _connection.UpdateAsync(countryLocalDto);
        else
            await _connection.InsertAsync(countryLocalDto);
        return countryLocalDto.Id;
    }

    public async Task<IEnumerable<CountryLocalDto>> GetAllCountries()
    {
        await _connection.CreateTableAsync<CountryLocalDto>();
        return await _connection.Table<CountryLocalDto>().ToListAsync();
    }
}
