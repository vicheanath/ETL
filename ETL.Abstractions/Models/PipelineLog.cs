namespace ETL.Abstractions.Models;

public class PipelineLog
{
    public Guid Id { get; set; }
    public Guid PipelineConfigId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; }
    public string LogDetails { get; set; }
}