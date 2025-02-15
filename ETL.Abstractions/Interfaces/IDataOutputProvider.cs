namespace ETL.Core.Interfaces;

public interface IDataOutputProvider
{
    Task LoadDataAsync(IEnumerable<dynamic> data, string configJson);

}