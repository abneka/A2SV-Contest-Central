using MediatR;

namespace Application.Features.Question.Queries.CheckDuplicate;

public class CheckDuplicateQuestionRequest : IRequest<bool>
{
    
    public string GlobalQuestionUrl { get; set; } = null!;
    
}