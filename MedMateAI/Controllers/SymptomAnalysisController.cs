using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.SymptomAnalysis.Requests;
using MedMateAI.Application.DTOs.SymptomAnalysis.Responses;
using MedMateAI.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedMateAI.Controllers;

[ApiController]
[Route("api/symptom-analysis")]
public sealed class SymptomAnalysisController : ControllerBase
{
    private const int MaxMessageLength = 2000;

    private readonly ISymptomAnalysisService _symptomAnalysisService;
    private readonly ILogger<SymptomAnalysisController> _logger;

    public SymptomAnalysisController(
        ISymptomAnalysisService symptomAnalysisService,
        ILogger<SymptomAnalysisController> logger)
    {
        _symptomAnalysisService = symptomAnalysisService;
        _logger = logger;
    }

    [HttpPost("analyze")]
    [ProducesResponseType(typeof(ApiResponse<SymptomAnalysisResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<SymptomAnalysisResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<SymptomAnalysisResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Analyze(
        [FromBody] AnalyzeSymptomsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new ApiResponse<SymptomAnalysisResponse>
            {
                Success = false,
                Message = "Analyze symptoms failed.",
                Errors = new List<string> { "Message is required." },
            });
        }

        if (request.Message.Trim().Length > MaxMessageLength)
        {
            return BadRequest(new ApiResponse<SymptomAnalysisResponse>
            {
                Success = false,
                Message = "Analyze symptoms failed.",
                Errors = new List<string> { $"Message must be {MaxMessageLength} characters or fewer." },
            });
        }

        try
        {
            var data = await _symptomAnalysisService.AnalyzeAsync(request, cancellationToken);
            return Ok(new ApiResponse<SymptomAnalysisResponse>
            {
                Success = true,
                Message = "OK",
                Data = data,
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<SymptomAnalysisResponse>
            {
                Success = false,
                Message = "Analyze symptoms failed.",
                Errors = new List<string> { ex.Message },
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Symptom analysis could not complete.");

            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<SymptomAnalysisResponse>
            {
                Success = false,
                Message = "Symptom analysis is unavailable.",
                Errors = new List<string>
                {
                    "Unable to analyze symptoms at this time. Please try again later.",
                },
            });
        }
    }

    [HttpGet("{sessionId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<SymptomAnalysisResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<SymptomAnalysisResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        var data = await _symptomAnalysisService.GetSessionByIdAsync(sessionId, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<SymptomAnalysisResponse>
            {
                Success = false,
                Message = "Symptom analysis session not found.",
            });
        }

        return Ok(new ApiResponse<SymptomAnalysisResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }
}
