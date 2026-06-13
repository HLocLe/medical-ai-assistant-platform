using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.IcdChapters.Requests;
using MedMateAI.Application.DTOs.IcdChapters.Responses;
using MedMateAI.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace MedMateAI.Controllers;

[ApiController]
[Route("api/icd-chapters")]
public sealed class IcdChaptersController : ControllerBase
{
    private readonly IIcdChapterService _icdChapterService;

    public IcdChaptersController(IIcdChapterService icdChapterService)
    {
        _icdChapterService = icdChapterService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<IcdChapterResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] PaginationQuery query,
        [FromQuery] string? search,
        CancellationToken cancellationToken = default)
    {
        var data = await _icdChapterService.ListIcdChaptersAsync(
            query.PageNumber,
            query.PageSize,
            search,
            cancellationToken);

        return Ok(new ApiResponse<PagedResponse<IcdChapterResponse>>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "Invalid ICD chapter id.",
            });
        }

        var data = await _icdChapterService.GetIcdChapterByIdAsync(id, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "ICD chapter not found.",
            });
        }

        return Ok(new ApiResponse<IcdChapterResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateIcdChapterRequest request,
        CancellationToken cancellationToken)
    {
        var (ok, errors, data) = await _icdChapterService.CreateIcdChapterAsync(request, cancellationToken);
        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "Create ICD chapter failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<IcdChapterResponse>
        {
            Success = true,
            Message = "ICD chapter created.",
            Data = data,
        });
    }

    [HttpPost("bulk")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<IcdChapterResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<IcdChapterResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BulkCreate(
        [FromBody] BulkCreateIcdChaptersRequest request,
        CancellationToken cancellationToken)
    {
        var (ok, errors, data) = await _icdChapterService.BulkCreateIcdChaptersAsync(request, cancellationToken);
        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<IReadOnlyList<IcdChapterResponse>>
            {
                Success = false,
                Message = "Bulk create ICD chapters failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<IReadOnlyList<IcdChapterResponse>>
        {
            Success = true,
            Message = $"{data.Count} ICD chapter(s) created.",
            Data = data,
        });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<IcdChapterResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateIcdChapterRequest request,
        CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "Invalid ICD chapter id.",
            });
        }

        var (ok, notFound, errors, data) = await _icdChapterService.UpdateIcdChapterAsync(id, request, cancellationToken);

        if (notFound)
        {
            return NotFound(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "ICD chapter not found.",
            });
        }

        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<IcdChapterResponse>
            {
                Success = false,
                Message = "Update ICD chapter failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<IcdChapterResponse>
        {
            Success = true,
            Message = "ICD chapter updated.",
            Data = data,
        });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Invalid ICD chapter id.",
            });
        }

        var (ok, notFound, errors) = await _icdChapterService.SoftDeleteIcdChapterAsync(id, cancellationToken);

        if (notFound)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "ICD chapter not found.",
            });
        }

        if (!ok)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Delete ICD chapter failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "ICD chapter deleted (soft).",
        });
    }
}
