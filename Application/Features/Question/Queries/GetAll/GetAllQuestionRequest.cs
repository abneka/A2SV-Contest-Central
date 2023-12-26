using Application.DTOs.Question;
using MediatR;

namespace Application.Features.Question.Queries.GetAll;

public class GetAllQuestionRequest : IRequest<IReadOnlyList<QuestionResponseDto>>
{
    
}