using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;
using MedMateAI.Application.IService;
using MedMateAI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedMateAI.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/disease-prior-probabilities")]
public sealed class DiseasePriorProbabilitiesController : ControllerBase
{
    private const string InvalidIdMessage = "Id P(A) không hợp lệ";
    private const string NotFoundMessage = "Không tìm thấy bản ghi P(A)";

    private readonly IDiseasePriorProbabilityService _service;

    public DiseasePriorProbabilitiesController(IDiseasePriorProbabilityService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<DiseasePriorProbabilityResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] PaginationQuery query,
        [FromQuery] string? search,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken = default)
    {
        var data = await _service.ListAsync(
            query.PageNumber,
            query.PageSize,
            search,
            isActive,
            cancellationToken);

        return Ok(ApiResponseFactory.Success(data, "OK"));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<DiseasePriorProbabilityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DiseasePriorProbabilityResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<DiseasePriorProbabilityResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(ApiResponseFactory.Fail<DiseasePriorProbabilityResponse>(InvalidIdMessage));
        }

        var data = await _service.GetByIdAsync(id, cancellationToken);
        if (data is null)
        {
            return NotFound(ApiResponseFactory.Fail<DiseasePriorProbabilityResponse>(NotFoundMessage));
        }

        return Ok(ApiResponseFactory.Success(data, "OK"));
    }

    [HttpPost("bulk")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<DiseasePriorProbabilityResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<DiseasePriorProbabilityResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BulkCreate(
        [FromBody] BulkCreateDiseasePriorProbabilitiesRequest request,
        CancellationToken cancellationToken)
    {
        var (ok, errors, data) = await _service.BulkCreateAsync(request, cancellationToken);
        if (!ok || data is null)
        {
            return BadRequest(ApiResponseFactory.FailFromErrors<IReadOnlyList<DiseasePriorProbabilityResponse>>(
                errors,
                "Bulk create P(A) thất bại"));
        }

        return Ok(ApiResponseFactory.Success(data, $"Đã tạo {data.Count} bản ghi P(A)."));
    }
}
