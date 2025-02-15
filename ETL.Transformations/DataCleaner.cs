using ETL.Core.Interfaces;

namespace ETL.Transformations;

public class DataCleaner : ITransformationService
{
    public Task<IEnumerable<dynamic>> ApplyTransformationAsync(IEnumerable<dynamic> data, string configJson)
    {
        // Remove null/empty records
        var cleaned = data.Where(d => d != null).ToList();
        return Task.FromResult(cleaned.AsEnumerable());
    }
}