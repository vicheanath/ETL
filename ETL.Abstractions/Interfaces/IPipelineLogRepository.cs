using ETL.Abstractions.Models;

namespace ETL.Abstractions.Interfaces;

public interface IPipelineLogRepository : IRepository<PipelineLog>
{
    Task LogPipelineExecutionAsync(Guid pipelineId, string status, string details);
}