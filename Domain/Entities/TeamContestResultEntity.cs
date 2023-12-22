using Domain.Common;

namespace Domain.Entities;

public class TeamContestResultEntity : BaseDomainEntity
{
    public Guid TeamId { get; set; }
    public Guid ContestId { get; set; }
    public float points { get; set; }
    public int rank { get; set; }
    public int penalty { get; set; }
    public int successfulHackCount { get; set; }
    public int unsuccessfulHackCount { get; set; }
    public bool isVirtual { get; set; }
    public TeamEntity? Team { get; set; }
    public ContestEntity? Contest { get; set; }
}