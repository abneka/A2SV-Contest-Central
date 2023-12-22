using Domain.Common;

namespace Domain.Entities;

public class UserContestResultEntity : BaseDomainEntity
{
    public Guid UserId { get; set; }
    public Guid ContestId { get; set; }
    public float points { get; set; }
    public int rank { get; set; }
    public int penalty { get; set; }
    public int successfulHackCount { get; set; }
    public int unsuccessfulHackCount { get; set; }
    public bool isVirtual { get; set; }
    public UserEntity User { get; set; } = null!;
    public ContestEntity Contest { get; set; } = null!;
}