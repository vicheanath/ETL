using ETL.Abstractions.Interfaces;
using ETL.Abstractions.Models;

namespace ETL.Database.Repositories;

public class PipelineLogRepository : IPipelineLogRepository
{
    private readonly ETLDbContext _context;

    public PipelineLogRepository(ETLDbContext context)
    {
        _context = context;
    }

    public async Task LogPipelineExecutionAsync(Guid pipelineId, string status, string details)
    {
        var log = new PipelineLog
        {
            PipelineConfigId = pipelineId,
            StartTime = DateTime.UtcNow,
            Status = status,
            LogDetails = details
        };
        await _context.PipelineLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    // Implement other IRepository methods...
    public Task<PipelineLog> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PipelineLog>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(PipelineLog entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(PipelineLog entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}