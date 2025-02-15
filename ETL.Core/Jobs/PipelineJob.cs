using ETL.Core.Interfaces;
using ETL.Database;
using Quartz;

namespace ETL.Core.Jobs;

public class PipelineJob : IJob
{
    private readonly IPipelineOrchestrator _orchestrator;
    private readonly ETLDbContext _dbContext;

    public PipelineJob(IPipelineOrchestrator orchestrator, ETLDbContext dbContext)
    {
        _orchestrator = orchestrator;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var pipelineId = new Guid(context.JobDetail.JobDataMap.GetString("PipelineId"));
        await _orchestrator.ExecutePipelineAsync(pipelineId);
    }
}