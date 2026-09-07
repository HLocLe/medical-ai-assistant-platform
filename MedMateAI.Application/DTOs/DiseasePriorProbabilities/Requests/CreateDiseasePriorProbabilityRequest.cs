namespace MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;

public sealed class CreateDiseasePriorProbabilityRequest
{
    public string Icd10Code { get; set; } = string.Empty;

    public string? DiseaseName { get; set; }

    public double PA { get; set; }

    public bool IsActive { get; set; } = true;
}
