using System.Net.Http.Json;
using System.Text.Json;
using ETL.Core.Interfaces;

namespace ETL.DataSources;

public class ApiDataSource : IDataSourceProvider
{
    private readonly HttpClient _httpClient;
    
    public ApiDataSource(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IEnumerable<dynamic>> ExtractDataAsync(string configJson)
    {
        var config = JsonSerializer.Deserialize<ApiConfig>(configJson);
        return await _httpClient.GetFromJsonAsync<IEnumerable<dynamic>>(config.Endpoint);
    }
}

public class ApiConfig
{
    public string Endpoint { get; set; }
}