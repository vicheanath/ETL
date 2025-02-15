using ETL.Abstractions.Interfaces;
using ETL.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Repositories;

public class PipelineConfigRepository : IPipelineConfigRepository
{
    private readonly ETLDbContext _context;

    public PipelineConfigRepository(ETLDbContext context)
    {
        _context = context;
    }

    public async Task<PipelineConfig> GetByIdAsync(Guid id)
    {
        return await _context.PipelineConfigs.FindAsync(id);
    }

    public async Task<IEnumerable<PipelineConfig>> GetAllAsync()
    {
        return await _context.PipelineConfigs.ToListAsync();
    }

    public async Task<IEnumerable<PipelineConfig>> GetActivePipelinesAsync()
    {
        return await _context.PipelineConfigs
            .Where(p => !string.IsNullOrEmpty(p.ScheduleCron))
            .ToListAsync();
    }

    public async Task AddAsync(PipelineConfig entity)
    {
        await _context.PipelineConfigs.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PipelineConfig entity)
    {
        _context.PipelineConfigs.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.PipelineConfigs.FindAsync(id);
        if (entity != null)
        {
            _context.PipelineConfigs.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}