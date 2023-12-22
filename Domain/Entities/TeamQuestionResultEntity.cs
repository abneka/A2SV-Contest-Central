using Domain.Common;

namespace Domain.Entities;

public class TeamQuestionResultEntity : BaseDomainEntity
{
    public Guid TeamId { get; set; }
    public Guid QuestionId { get; set; }
    public float points { get; set; }
    public int rejectAttemptCount { get; set; }
    public String bestSubmissionTimeSeconds { get; set; }
    public bool isVirtual { get; set; }
    public TeamEntity? Team { get; set; }
    public QuestionEntity? Question { get; set; }
}