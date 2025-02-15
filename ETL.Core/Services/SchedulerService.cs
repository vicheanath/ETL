using ETL.Core.Jobs;
using ETL.Database;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ETL.Core.Services;

public class SchedulerService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ETLDbContext _dbContext;

    public SchedulerService(ISchedulerFactory schedulerFactory, ETLDbContext dbContext)
    {
        _schedulerFactory = schedulerFactory;
        _dbContext = dbContext;
    }

    public async Task ScheduleAllPipelinesAsync()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var pipelines = await _dbContext.PipelineConfigs.ToListAsync();

        foreach (var pipeline in pipelines)
        {
            var job = JobBuilder.Create<PipelineJob>()
                .WithIdentity(pipeline.Id.ToString())
                .UsingJobData("PipelineId", pipeline.Id.ToString())
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(pipeline.ScheduleCron)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}