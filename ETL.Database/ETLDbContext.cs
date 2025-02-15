using System.Text.Json;
using ETL.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
namespace ETL.Database;

public class ETLDbContext : DbContext
{
    public ETLDbContext(DbContextOptions<ETLDbContext> options) : base(options) { }

    public DbSet<PipelineConfig> PipelineConfigs { get; set; }
    public DbSet<PipelineLog> PipelineLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ETLDbContext).Assembly);
    }
}