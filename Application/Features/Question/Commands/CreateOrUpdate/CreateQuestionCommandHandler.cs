using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Question.Commands.CreateOrUpdate
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateOrUpdateQuestionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateQuestionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateOrUpdateQuestionCommand command, CancellationToken cancellationToken)
        {


            List<QuestionEntity> questionList = command.NewQuestions.Questions.Select((question)=>
            {
                var questionEntity = new QuestionEntity
                {
                    GlobalQuestionUrl = question.Url,
                    Index = question.Index,
                    ContestId = command.NewQuestions.ContestId
                };
                return questionEntity;
            })
            .ToList();

            await _unitOfWork.QuestionRepository.CreateListAsync(questionList);
            return true;
        }
    }
}

        // RuleFor(dto => dto.Questions)
        //     .NotNull()
        //     .WithMessage("Questions list cannot be null.")
        //     .NotEmpty()
        //     .WithMessage("At least one question is required.");