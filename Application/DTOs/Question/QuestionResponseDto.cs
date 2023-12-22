using Application.DTOs.Common;

namespace Application.DTOs.Question;

public class QuestionResponseDto : BaseDto
{
    public Guid ContestId { get; set; }
    
    public string GlobalQuestionUrl { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    
    public string Index { get; set; } = string.Empty;
    
    public List<UserQuestionResultResponseDto> UserQuestionResults { get; set; } = new List<UserQuestionResultResponseDto>();
    
    public List<TeamQuestionResultResponseDto> TeamQuestionResults { get; set; } = new List<TeamQuestionResultResponseDto>();
}