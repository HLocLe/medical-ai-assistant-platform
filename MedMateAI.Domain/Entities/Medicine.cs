namespace MedMateAI.Domain.Entities;

public sealed class Medicine : BaseEntity
{
    public string? MedicineName { get; set; }

    public string? ActiveIngredient { get; set; }

    public string? DosageForm { get; set; }

    public string? Strength { get; set; }

    public string? Manufacturer { get; set; }

    public string? Description { get; set; }

    public ICollection<MedicationScanResult> MedicationScanResults { get; set; } = new List<MedicationScanResult>();

    public ICollection<UserMedication> UserMedications { get; set; } = new List<UserMedication>();

    public ICollection<DrugAnalysisResult> DrugAnalysisResults { get; set; } = new List<DrugAnalysisResult>();
}
