using System.Text.Json.Serialization;

namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses.MedGemma;

internal sealed class MedGemmaDiagnosesJsonResponse
{
    [JsonPropertyName("diagnoses")]
    public List<MedGemmaDiagnosisJsonItem> Diagnoses { get; set; } = [];
}
