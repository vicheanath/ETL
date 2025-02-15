namespace ETL.Core.Interfaces;

public interface ITransformationService
{
    Task<IEnumerable<dynamic>> ApplyTransformationAsync(IEnumerable<dynamic> data, string configJson);
}