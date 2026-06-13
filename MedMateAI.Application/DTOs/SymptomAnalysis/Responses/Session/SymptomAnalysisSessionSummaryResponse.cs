using MedMateAI.Domain.Enums;

namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.Session;

public sealed class SymptomAnalysisSessionSummaryResponse
{
    public Guid SessionId { get; set; }

    public string? InputText { get; set; }

    public string? SeverityLevel { get; set; }

    public SymptomAnalysisSessionStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
