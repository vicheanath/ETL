using ETL.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETL.Database.Configurations;

public class PipelineLogConfigurations : IEntityTypeConfiguration<PipelineLog>
{
    public void Configure(EntityTypeBuilder<PipelineLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PipelineConfigId);
        builder.Property(x => x.StartTime);
        builder.Property(x => x.EndTime);
        builder.Property(x => x.Status);
        builder.Property(x => x.LogDetails);
    }
}