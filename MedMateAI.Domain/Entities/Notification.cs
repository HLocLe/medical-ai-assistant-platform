namespace MedMateAI.Domain.Entities;

public sealed class Notification : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid ReminderId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Channel { get; set; }

    public string? Status { get; set; }

    public DateTime? SentAt { get; set; }

    public FollowUpReminder Reminder { get; set; } = null!;
}
