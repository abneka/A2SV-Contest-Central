using Domain.Common;

namespace Domain.Entities;

public class UserContestResultEntity : BaseDomainEntity
{
    public Guid UserId { get; set; }
    public Guid ContestId { get; set; }
    public float Points { get; set; }
    public int Rank { get; set; }
    public int Penalty { get; set; }
    public int SuccessfulHackCount { get; set; }
    public int UnsuccessfulHackCount { get; set; }
    public bool IsVirtual { get; set; }
    public UserEntity User { get; set; } = null!;
    public ContestEntity Contest { get; set; } = null!;
}