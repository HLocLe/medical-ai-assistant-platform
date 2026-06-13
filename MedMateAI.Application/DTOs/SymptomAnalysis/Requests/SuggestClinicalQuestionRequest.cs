namespace MedMateAI.Application.DTOs.SymptomAnalysis.Requests;

public sealed class SuggestClinicalQuestionRequest
{
    public string UserInput { get; set; } = string.Empty;
}
