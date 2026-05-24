namespace MedMateAI.Application.DTOs.SymptomAnalysis.Responses;

public sealed class RecommendedFacilityResponse
{
    public Guid FacilityId { get; set; }

    public string? FacilityName { get; set; }

    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? OpeningHours { get; set; }

    public string? FacilityType { get; set; }

    public Guid DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public double? ConfidenceScore { get; set; }

    public int PriorityRank { get; set; }
}
