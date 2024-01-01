using Application.DTOs.GlobalQuestion;
using MediatR;

namespace Application.Features.Question.Queries.CheckDuplicate;

public class CheckDuplicateQuestionRequest : IRequest<GlobalQuestionDto>
{
    
    public string GlobalQuestionUrl { get; set; } = null!;
    
}