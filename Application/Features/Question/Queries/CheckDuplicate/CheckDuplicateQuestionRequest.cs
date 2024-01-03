using Application.DTOs.GlobalQuestion;
using Application.DTOs.Question;
using MediatR;

namespace Application.Features.Question.Queries.CheckDuplicate;

public class CheckDuplicateQuestionRequest : IRequest<QuestionDuplicateCheckResponseDto>
{
    public string GlobalQuestionUrl { get; set; } = null!;
}