namespace MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;

public sealed class BulkCreateDiseasePriorProbabilitiesRequest
{
    public IList<CreateDiseasePriorProbabilityRequest> Items { get; set; } =
        new List<CreateDiseasePriorProbabilityRequest>();
}
