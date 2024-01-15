using Domain.Common;

namespace Domain.Entities;

public class QuestionEntity : BaseDomainEntity
{
    public string GlobalQuestionUrl { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    
    public string Index { get; set; } = string.Empty;
    public int Rating { get; set; }
    
    public Guid ContestId { get; set; }
    
    public ContestEntity Contest { get; set; } = null!;
    
    public List<UserContestResultEntity> UserContestResult { get; set; } = new List<UserContestResultEntity>();
    public List<UserQuestionResultEntity> UserQuestionResults { get; set; } = new List<UserQuestionResultEntity>();

    public List<TeamQuestionResultEntity> TeamQuestionResults { get; set; } = new List<TeamQuestionResultEntity>();
}