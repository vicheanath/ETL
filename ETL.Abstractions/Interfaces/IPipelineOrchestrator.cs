using ETL.Core.Models;

namespace ETL.Core.Interfaces;

public interface IPipelineOrchestrator
{
    Task<PipelineResult> ExecutePipelineAsync(Guid pipelineId);
}