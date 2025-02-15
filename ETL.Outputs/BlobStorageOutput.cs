using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using ETL.Core.Interfaces;

namespace ETL.Outputs;

public class BlobStorageOutput : IDataOutputProvider
{
    public async Task LoadDataAsync(IEnumerable<dynamic> data, string configJson)
    {
        var config = JsonSerializer.Deserialize<BlobConfig>(configJson);
        var blobClient = new BlobClient(config.ConnectionString, config.ContainerName, config.BlobName);
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        await blobClient.UploadAsync(stream, overwrite: true);
    }
}

public class BlobConfig
{
    public string ConnectionString { get; set; }
    public string ContainerName { get; set; }
    public string BlobName { get; set; }
}