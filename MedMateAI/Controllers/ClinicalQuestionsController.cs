using MedMateAI.Application.DTOs.ClinicalQuestions.Requests;
using MedMateAI.Application.DTOs.ClinicalQuestions.Responses;
using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace MedMateAI.Controllers;

[ApiController]
[Route("api/clinical-questions")]
public sealed class ClinicalQuestionsController : ControllerBase
{
    private readonly IClinicalQuestionService _clinicalQuestionService;

    public ClinicalQuestionsController(IClinicalQuestionService clinicalQuestionService)
    {
        _clinicalQuestionService = clinicalQuestionService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ClinicalQuestionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ClinicalQuestionResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        [FromQuery] PaginationQuery query,
        [FromQuery] Guid? chapterId,
        [FromQuery] string? search,
        CancellationToken cancellationToken = default)
    {
        if (chapterId.HasValue && chapterId.Value == Guid.Empty)
        {
            return BadRequest(new ApiResponse<PagedResponse<ClinicalQuestionResponse>>
            {
                Success = false,
                Message = "Invalid ICD chapter id.",
            });
        }

        var data = await _clinicalQuestionService.ListClinicalQuestionsAsync(
            query.PageNumber,
            query.PageSize,
            chapterId,
            search,
            cancellationToken);

        return Ok(new ApiResponse<PagedResponse<ClinicalQuestionResponse>>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Invalid clinical question id.",
            });
        }

        var data = await _clinicalQuestionService.GetClinicalQuestionByIdAsync(id, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Clinical question not found.",
            });
        }

        return Ok(new ApiResponse<ClinicalQuestionResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateClinicalQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var (ok, errors, data) = await _clinicalQuestionService.CreateClinicalQuestionAsync(request, cancellationToken);
        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Create clinical question failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<ClinicalQuestionResponse>
        {
            Success = true,
            Message = "Clinical question created.",
            Data = data,
        });
    }

    [HttpPost("bulk")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ClinicalQuestionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ClinicalQuestionResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BulkCreate(
        [FromBody] BulkCreateClinicalQuestionsRequest request,
        CancellationToken cancellationToken)
    {
        var (ok, errors, data) = await _clinicalQuestionService.BulkCreateClinicalQuestionsAsync(request, cancellationToken);
        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<IReadOnlyList<ClinicalQuestionResponse>>
            {
                Success = false,
                Message = "Bulk create clinical questions failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<IReadOnlyList<ClinicalQuestionResponse>>
        {
            Success = true,
            Message = $"{data.Count} clinical question(s) created.",
            Data = data,
        });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClinicalQuestionResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateClinicalQuestionRequest request,
        CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Invalid clinical question id.",
            });
        }

        var (ok, notFound, errors, data) = await _clinicalQuestionService.UpdateClinicalQuestionAsync(id, request, cancellationToken);

        if (notFound)
        {
            return NotFound(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Clinical question not found.",
            });
        }

        if (!ok || data is null)
        {
            return BadRequest(new ApiResponse<ClinicalQuestionResponse>
            {
                Success = false,
                Message = "Update clinical question failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse<ClinicalQuestionResponse>
        {
            Success = true,
            Message = "Clinical question updated.",
            Data = data,
        });
    }

    [HttpDelete("{id:guid}")]
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
                Message = "Invalid clinical question id.",
            });
        }

        var (ok, notFound, errors) = await _clinicalQuestionService.SoftDeleteClinicalQuestionAsync(id, cancellationToken);

        if (notFound)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Clinical question not found.",
            });
        }

        if (!ok)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Delete clinical question failed.",
                Errors = errors.ToList(),
            });
        }

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Clinical question deleted (soft).",
        });
    }
}
