using ETL.Abstractions.Interfaces;
using ETL.Core.Interfaces;
using ETL.Core.Models;
using ETL.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ETL.Core.Services;

public class PipelineOrchestrator : IPipelineOrchestrator
{
    private readonly IPipelineConfigRepository _configRepository;
    private readonly IPipelineLogRepository _logRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PipelineOrchestrator> _logger;

    public PipelineOrchestrator(
        IPipelineConfigRepository configRepository,
        IPipelineLogRepository logRepository,
        IServiceProvider serviceProvider,
        ILogger<PipelineOrchestrator> logger)
    {
        _configRepository = configRepository;
        _logRepository = logRepository;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<PipelineResult> ExecutePipelineAsync(Guid pipelineId)
    {
        var pipelineConfig = await _configRepository.GetByIdAsync(pipelineId);
        if (pipelineConfig == null)
            throw new ArgumentException("Pipeline not found.");

        try
        {
            // Execute pipeline logic...
            // 1. Extract
            var dataSource = _serviceProvider.GetRequiredService(
                Type.GetType(pipelineConfig.DataSourceType)) as IDataSourceProvider;
            var extractedData = await dataSource.ExtractDataAsync(pipelineConfig.DataSourceConfig);

            // 2. Transform
            var transformations = pipelineConfig.Transformations.Split(',');
            foreach (var transformationType in transformations)
            {
                var transformation = _serviceProvider.GetRequiredService(
                    Type.GetType(transformationType)) as ITransformationService;
                extractedData = await transformation.ApplyTransformationAsync(extractedData, null);
            }

            // 3. Load
            var output = _serviceProvider.GetRequiredService(
                Type.GetType(pipelineConfig.OutputType)) as IDataOutputProvider;
            await output.LoadDataAsync(extractedData, pipelineConfig.OutputConfig);
            await _logRepository.LogPipelineExecutionAsync(pipelineId, "Success", "Pipeline executed successfully.");
            return new PipelineResult { IsSuccess = true };
        }
        catch (Exception ex)
        {
            await _logRepository.LogPipelineExecutionAsync(pipelineId, "Failed", ex.Message);
            return new PipelineResult { IsSuccess = false, Error = ex.Message };
        }
    }
}