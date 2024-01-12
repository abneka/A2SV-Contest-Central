using Application.DTOs.Common;
using Application.DTOs.User;

namespace Application.DTOs.UserQuestionResult;

public class UserQuestionsResultResponseDto : QuestionResultResponseDto
{
    public Guid UserId { get; set; }
    public UserResponseWithoutUserQuestionsDto User { get; set; } = null!;
}