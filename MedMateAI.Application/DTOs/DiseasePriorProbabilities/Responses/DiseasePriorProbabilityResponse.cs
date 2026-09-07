namespace MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;

public sealed class DiseasePriorProbabilityResponse
{
    public Guid Id { get; set; }

    public string Icd10Code { get; set; } = string.Empty;

    public string? DiseaseName { get; set; }

    public double PA { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
