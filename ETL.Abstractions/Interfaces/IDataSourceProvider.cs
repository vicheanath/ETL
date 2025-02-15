namespace ETL.Core.Interfaces;

public interface IDataSourceProvider
{
    Task<IEnumerable<dynamic>> ExtractDataAsync(string configJson);

}