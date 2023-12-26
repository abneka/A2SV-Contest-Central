using Application.DTOs.Question;
using MediatR;

namespace Application.Features.Question.Queries.GetSingle;

public class GetSingleQuestionRequest : IRequest<QuestionResponseDto>
{
    public Guid QuestionId { get; set; }
}