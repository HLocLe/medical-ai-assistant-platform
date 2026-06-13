namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.ClinicalQuestions;

public sealed class ClinicalQuestionAnswersResponse
{
    public Guid SessionId { get; set; }

    public string UserInput { get; set; } = string.Empty;

    public IReadOnlyList<ClinicalQuestionAnswerResult> Answers { get; set; } =
        Array.Empty<ClinicalQuestionAnswerResult>();

    public SymptomAnalysisAnalyzeResponse? Analysis { get; set; }
}
