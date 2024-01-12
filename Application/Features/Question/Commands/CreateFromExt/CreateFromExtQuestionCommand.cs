using Application.DTOs.Question;
using MediatR;

namespace Application.Features.Question.Commands.CreateFromExt
{
    public class CreateFromExtQuestionCommand : IRequest<bool>
    {
        public QuestionRequestDto NewQuestions { get; set; } = null!;
    }
}