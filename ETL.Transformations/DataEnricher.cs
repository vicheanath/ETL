using System.Net.Http.Json;
using System.Text.Json;
using ETL.Core.Interfaces;

namespace ETL.Transformations;

public class DataEnricher : ITransformationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DataEnricher(IHttpClientFactory httpClientFactory) => 
        _httpClientFactory = httpClientFactory;

    public async Task<IEnumerable<dynamic>> ApplyTransformationAsync(IEnumerable<dynamic> data, string configJson)
    {
        var config = JsonSerializer.Deserialize<EnrichmentConfig>(configJson);
        var client = _httpClientFactory.CreateClient();
        foreach (var item in data)
        {
            var response = await client.GetFromJsonAsync<dynamic>($"{config.ApiUrl}/{item.Id}");
            item.EnrichedField = response.Value;
        }
        return data;
    }
}

public class EnrichmentConfig
{
    public string ApiUrl { get; set; }
}