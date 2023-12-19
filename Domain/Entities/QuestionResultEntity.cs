using Domain.Common;

namespace Domain.Entities;

public class QuestionResultEntity : BaseDomainEntity
{
    public double Points { get; set; }
    public int RejectedAttemptCount { get; set; }
    public int BestSubmissionTimeSeconds { get; set; }
}