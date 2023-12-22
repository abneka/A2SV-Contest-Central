using Application.DTOs.Common;
using Application.DTOs.User;

namespace Application.DTOs.QuestionResult;

public class UserQuestionResultDto : BaseDto
{
    public double Points { get; set; }
    public int RejectedAttemptCount { get; set; }
    public int BestSubmissionTimeSeconds { get; set; }
    public Guid UserId { get; set; }
    public UserResponseDto User { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public QuestionResponseDto Question { get; set; } = null!;
}