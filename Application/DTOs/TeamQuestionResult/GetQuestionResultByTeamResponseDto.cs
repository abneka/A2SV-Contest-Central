using Application.DTOs.Common;
using Application.DTOs.Question;
using Application.DTOs.Team;

namespace Application.DTOs.TeamQuestionResult;

public class GetQuestionResultByTeamResponseDto : BaseDto
{
    public float Points { get; set; }
    public int RejectAttemptCount { get; set; }
    public String BestSubmissionTimeSeconds { get; set; } = null!;
    public bool IsVirtual { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamResponseDto Team { get; set; } = null!;
    
    public Guid QuestionId { get; set; }
    public QuestionResponseDto Question { get; set; } = null!;
}