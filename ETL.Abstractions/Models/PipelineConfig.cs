namespace ETL.Abstractions.Models;

public class PipelineConfig
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DataSourceType { get; set; }
    public string DataSourceConfig { get; set; }
    public string Transformations { get; set; }
    public string OutputType { get; set; }
    public string OutputConfig { get; set; }
    public string ScheduleCron { get; set; }
}