using Application.DTOs.Question;
using MediatR;

namespace Application.Features.Question.Commands.CreateOrUpdate
{
    public class CreateOrUpdateQuestionCommand : IRequest<bool>
    {
        public QuestionRequestDto NewQuestions { get; set; } = null!;
    }
}