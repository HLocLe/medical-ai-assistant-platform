using MedMateAI.Application.DTOs.MedicalFacilities.Responses;
using MedMateAI.Application.DTOs.SymptomAnalysis.Responses.Session;
using MedMateAI.Domain.Enums;
namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.ClinicalQuestions;

public sealed class SymptomAnalysisAnalyzeResponse
{
    public Guid SessionId { get; set; }

    public SymptomAnalysisSessionStatus Status { get; set; }

    public string? Model { get; set; }

    public IReadOnlyList<BayesianDiagnosisResponse> Diagnoses { get; set; } =
        Array.Empty<BayesianDiagnosisResponse>();

    public BayesianDiagnosisResponse? PrimaryDiagnosis { get; set; }

    public RecommendedDepartmentResponse? RecommendedDepartment { get; set; }

    public IReadOnlyList<MedicalFacilityResponse> RecommendedFacilities { get; set; } =
        Array.Empty<MedicalFacilityResponse>();
}