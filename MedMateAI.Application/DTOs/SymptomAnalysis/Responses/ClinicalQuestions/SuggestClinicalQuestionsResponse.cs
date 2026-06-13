namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.ClinicalQuestions;

public sealed class SuggestClinicalQuestionsResponse
{
    public Guid SessionId { get; set; }

    public IReadOnlyList<SuggestedClinicalQuestionResponse> Questions { get; set; } =
        Array.Empty<SuggestedClinicalQuestionResponse>();
}
