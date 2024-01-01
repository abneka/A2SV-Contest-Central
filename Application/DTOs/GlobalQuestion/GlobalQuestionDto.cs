using Application.DTOs.Common;

namespace Application.DTOs.GlobalQuestion;

public class GlobalQuestionDto : BaseDto
{
    public bool Status { get; set; }
    public List<string> Group { get; set; } = new List<string>();
    public int NumberOfTimesUsed { get; set; }
}