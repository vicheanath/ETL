using ETL.Abstractions.Models;

namespace ETL.Abstractions.Interfaces;

public interface IPipelineConfigRepository : IRepository<PipelineConfig>
{
    Task<IEnumerable<PipelineConfig>> GetActivePipelinesAsync();
}
