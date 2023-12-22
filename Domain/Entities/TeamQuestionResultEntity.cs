using Domain.Common;

namespace Domain.Entities;

public class TeamQuestionResultEntity : BaseDomainEntity
{
    public Guid TeamId { get; set; }
    public Guid QuestionId { get; set; }
    public float Points { get; set; }
    public int RejectAttemptCount { get; set; }
    public String BestSubmissionTimeSeconds { get; set; } = null!;
    public bool IsVirtual { get; set; }
    public TeamEntity Team { get; set; }
    public QuestionEntity Question { get; set; }
}