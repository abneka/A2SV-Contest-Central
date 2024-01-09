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

            var contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(command.NewQuestions.ContestId);
            if(contest == null){
                return false;
            }

            List<QuestionEntity> questionList = command.NewQuestions.Questions.Select((question, index)=>
            {
                var questionEntity = new QuestionEntity
                {
                    GlobalQuestionUrl = question,
                    Index = ((char)(65+index)).ToString(),
                    ContestId = contest.Id
                };
                return questionEntity;
            })
            .ToList();

            await _unitOfWork.QuestionRepository.CreateListAsync(questionList);
            return true;
        }
    }
}
