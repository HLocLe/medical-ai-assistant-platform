namespace MedMateAI.Application.DTOs.SymptomAnalysis.Requests;

public sealed class AnalyzeSymptomsRequest
{
    public string Message { get; set; } = string.Empty;

    public bool DisclaimerShown { get; set; } = true;
}
