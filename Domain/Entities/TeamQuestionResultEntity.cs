using Domain.Common;

namespace Domain.Entities;

public class TeamQuestionResultEntity : BaseDomainEntity
{
    public float Points { get; set; }
    public int RejectAttemptCount { get; set; }
    public String BestSubmissionTimeSeconds { get; set; } = null!;
    public bool IsVirtual { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamEntity Team { get; set; } = null!;
    
    public Guid QuestionId { get; set; }
    public QuestionEntity Question { get; set; } = null!;
}