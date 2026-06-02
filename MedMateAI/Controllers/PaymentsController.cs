using System.Text;
using MedMateAI.Application.DTOs.Common;
using MedMateAI.Application.DTOs.Payments.Responses;
using MedMateAI.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace MedMateAI.Controllers;

[ApiController]
[Route("api/payments")]
public sealed class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("payos-return")]
    [ProducesResponseType(typeof(ApiResponse<PayOSReturnResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PayOSReturn(CancellationToken cancellationToken = default)
    {
        var query = Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        var data = await _paymentService.ProcessPayOSReturnAsync(query, cancellationToken);

        return Ok(new ApiResponse<PayOSReturnResponse>
        {
            Success = data.Success,
            Message = data.Message,
            Data = data,
        });
    }

    [HttpGet("payos-cancel")]
    [ProducesResponseType(typeof(ApiResponse<PayOSReturnResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PayOSCancel(CancellationToken cancellationToken = default)
    {
        var query = Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        var data = await _paymentService.ProcessPayOSCancelAsync(query, cancellationToken);

        return Ok(new ApiResponse<PayOSReturnResponse>
        {
            Success = data.Success,
            Message = data.Message,
            Data = data,
        });
    }

    [HttpPost("payos-webhook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PayOSWebhook(CancellationToken cancellationToken = default)
    {
        var rawBody = await ReadRawBodyAsync(cancellationToken);
        var processed = await _paymentService.ProcessPayOSWebhookAsync(rawBody, cancellationToken);

        if (!processed)
        {
            return BadRequest("Invalid webhook");
        }

        return Ok("OK");
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PaymentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PaymentResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PaymentResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse<PaymentResponse>
            {
                Success = false,
                Message = "Invalid payment id.",
            });
        }

        var data = await _paymentService.GetPaymentByIdAsync(id, cancellationToken);
        if (data is null)
        {
            return NotFound(new ApiResponse<PaymentResponse>
            {
                Success = false,
                Message = "Payment not found.",
            });
        }

        return Ok(new ApiResponse<PaymentResponse>
        {
            Success = true,
            Message = "OK",
            Data = data,
        });
    }

    private async Task<string> ReadRawBodyAsync(CancellationToken cancellationToken)
    {
        Request.EnableBuffering();
        using var reader = new StreamReader(
            Request.Body,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);

        var body = await reader.ReadToEndAsync(cancellationToken);
        Request.Body.Position = 0;
        return body;
    }
}
