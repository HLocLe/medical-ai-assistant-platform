using MedMateAI.Application.DTOs.AIConfigs.Requests;
using MedMateAI.Application.DTOs.AIConfigs.Responses;
using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace MedMateAI.Controllers;

[ApiController]
[Route("api/ai-configs")]
public sealed class AIConfigsController : ControllerBase
{
    private readonly IAIConfigService _aiConfigService;

    public AIConfigsController(IAIConfigService aiConfigService)
    {
        _aiConfigService = aiConfigService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AIConfigResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] PaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var data = await _aiConfigService.ListAIConfigsAsync(query.PageNumber, query.PageSize, cancellationToken);
        return Ok(new ApiResponse<PagedResponse<AIConfigResponse>>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AIConfigResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListActive(CancellationToken cancellationToken = default)
    {
        var data = await _aiConfigService.ListActiveAIConfigsAsync(cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<AIConfigResponse>>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpGet("by-task-type/{taskType}")]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTaskType(string taskType, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(taskType))
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Invalid task type.",
            });
        }

        var data = await _aiConfigService.GetActiveAIConfigByTaskTypeAsync(taskType, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "AI config not found.",
            });
        }

        return Ok(new ApiResponse<AIConfigResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Invalid AI config id.",
            });
        }

        var data = await _aiConfigService.GetAIConfigByIdAsync(id, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "AI config not found.",
            });
        }

        return Ok(new ApiResponse<AIConfigResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAIConfigRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Create AI config failed.",
                Errors = new List<string> { "Request body is required." },
            });
        }

        try
        {
            var data = await _aiConfigService.CreateAIConfigAsync(request, cancellationToken);
            return Ok(new ApiResponse<AIConfigResponse>
            {
                Success = true,
                Message = "AI config created.",
                Data = data,
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Create AI config failed.",
                Errors = new List<string> { ex.Message },
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Create AI config failed.",
                Errors = new List<string> { ex.Message },
            });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateAIConfigRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Invalid AI config id.",
            });
        }

        if (request is null)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Update AI config failed.",
                Errors = new List<string> { "Request body is required." },
            });
        }

        try
        {
            var data = await _aiConfigService.UpdateAIConfigAsync(id, request, cancellationToken);
            if (data is null)
            {
                return NotFound(new ApiResponse<AIConfigResponse>
                {
                    Success = false,
                    Message = "AI config not found.",
                });
            }

            return Ok(new ApiResponse<AIConfigResponse>
            {
                Success = true,
                Message = "AI config updated.",
                Data = data,
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Update AI config failed.",
                Errors = new List<string> { ex.Message },
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Update AI config failed.",
                Errors = new List<string> { ex.Message },
            });
        }
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AIConfigResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        [FromBody] UpdateAIConfigStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Invalid AI config id.",
            });
        }

        if (request is null)
        {
            return BadRequest(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "Update AI config status failed.",
                Errors = new List<string> { "Request body is required." },
            });
        }

        var data = await _aiConfigService.UpdateAIConfigStatusAsync(id, request, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<AIConfigResponse>
            {
                Success = false,
                Message = "AI config not found.",
            });
        }

        return Ok(new ApiResponse<AIConfigResponse>
        {
            Success = true,
            Message = "AI config status updated.",
            Data = data,
        });
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = "Invalid AI config id.",
            });
        }

        var deleted = await _aiConfigService.DeleteAIConfigAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound(new ApiResponse<bool>
            {
                Success = false,
                Message = "AI config not found.",
                Data = false,
            });
        }

        return Ok(new ApiResponse<bool>
        {
            Success = true,
            Message = "AI config deleted (soft).",
            Data = true,
        });
    }
}
