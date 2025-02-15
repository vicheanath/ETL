using ETL.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETL.API.Controllers;

[ApiController]
[Route("api/pipelines")]
public class PipelineController : ControllerBase
{
    private readonly IPipelineOrchestrator _orchestrator;
    
    public PipelineController(IPipelineOrchestrator orchestrator) => 
        _orchestrator = orchestrator;

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartPipeline(Guid id)
    {
        var result = await _orchestrator.ExecutePipelineAsync(id);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}