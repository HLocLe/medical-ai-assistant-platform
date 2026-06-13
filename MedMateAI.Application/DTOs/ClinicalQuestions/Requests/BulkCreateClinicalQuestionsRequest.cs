namespace MedMateAI.Application.DTOs.ClinicalQuestions.Requests;

public sealed class BulkCreateClinicalQuestionsRequest
{
    public List<CreateClinicalQuestionRequest> Questions { get; set; } = new();
}
