using Application.DTOs.Question;
using Application.DTOs.Team;

namespace Application.DTOs.TeamQuestionResult;

public class TeamQuestionResultResponseDto
{
    public float Points { get; set; }
    public int RejectAttemptCount { get; set; }
    public String BestSubmissionTimeSeconds { get; set; } = null!;
    public bool IsVirtual { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamReponseDto Team { get; set; } = null!;
    
    public Guid QuestionId { get; set; }
    public QuestionResponseDto Question { get; set; } = null!;
}