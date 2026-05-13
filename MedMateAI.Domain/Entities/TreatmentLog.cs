namespace MedMateAI.Domain.Entities;

public sealed class TreatmentLog : BaseEntity
{
    public Guid RecoveryPlanId { get; set; }

    public int DayNumber { get; set; }

    public string? DailyTaskJson { get; set; }

    public string? MedicationInstruction { get; set; }

    public bool IsMedicationTaken { get; set; }

    public string? SymptomNote { get; set; }

    public double? Temperature { get; set; }

    public int? PainLevel { get; set; }

    public string? AiFeedbackNote { get; set; }

    public RecoveryPlan RecoveryPlan { get; set; } = null!;

    public ICollection<FollowUpReminder> FollowUpReminders { get; set; } = new List<FollowUpReminder>();
}
