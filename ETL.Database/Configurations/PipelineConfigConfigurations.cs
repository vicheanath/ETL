using ETL.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETL.Database.Configurations;

public class PipelineConfigConfigurations : IEntityTypeConfiguration<PipelineConfig>
{
    public void Configure(EntityTypeBuilder<PipelineConfig> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.DataSourceType).IsRequired();
        builder.Property(x => x.DataSourceConfig).IsRequired();
        builder.Property(x => x.Transformations).IsRequired();
        builder.Property(x => x.OutputType).IsRequired();
        builder.Property(x => x.OutputConfig).IsRequired();
        builder.Property(x => x.ScheduleCron).IsRequired();
    }
}