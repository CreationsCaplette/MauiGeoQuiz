using MauiGeoQuiz.Game.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MauiGeoQuiz.Game.Datasources;
public interface ICountriesDatasource
{
    Task<IEnumerable<CountryDataModel>> FetchCountriesData();
}

public class CountriesApiDatasource : ICountriesDatasource
{
    private const string BaseUrl = "https://restcountries.com/";
    private const string Parameters = "v3.1/all?fields=name,capital,region,subregion,flags";

    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public CountriesApiDatasource(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<IEnumerable<CountryDataModel>> FetchCountriesData()
    {
        var countries = new List<CountryDataModel>();

        var uri = new Uri(string.Format(Path.Combine(BaseUrl, Parameters), string.Empty));
        try
        {
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                countries = JsonSerializer.Deserialize<List<CountryDataModel>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return countries;
    }
}