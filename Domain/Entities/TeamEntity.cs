using Domain.Common;

namespace Domain.Entities;

public class TeamEntity : BaseDomainEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
    public GroupEntity? Group { get; set; }
    public List<TeamContestResultEntity> TeamContestResults { get; set; } = new List<TeamContestResultEntity>();
    public List<TeamQuestionResultEntity> TeamQuestionResults { get; set; } = new List<TeamQuestionResultEntity>();
}