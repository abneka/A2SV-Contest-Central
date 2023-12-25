using Domain.Common;

namespace Domain.Entities;

public class TeamEntity : BaseDomainEntity
{
    public string Name { get; set; } = string.Empty;
   
    public TeamContestResultEntity TeamContestResults { get; set; } = null!;
    public List<TeamQuestionResultEntity> TeamQuestionResults { get; set; } = new List<TeamQuestionResultEntity>();
    public List<UserEntity> Members { get; set; } = new List<UserEntity>();
}