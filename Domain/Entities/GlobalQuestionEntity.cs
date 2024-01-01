using Domain.Common;

namespace Domain.Entities;

public class GlobalQuestionEntity : BaseDomainEntity
{
    public bool Status { get; set; }
    public List<string> Group { get; set; } = new List<string>();
    public int NumberOfTimesUsed { get; set; }
}