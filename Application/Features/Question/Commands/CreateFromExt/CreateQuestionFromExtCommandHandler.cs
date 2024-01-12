using Application.Contracts.Persistence;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Features.Question.Commands.CreateFromExt
{
    public class CreateQuestionFromExtCommandHandler : IRequestHandler<CreateFromExtQuestionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateQuestionFromExtCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateFromExtQuestionCommand command, CancellationToken cancellationToken)
        {

            string global_contest_id = ParseIdFromUrl(command.NewQuestions.ContestUrl);
            var contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(global_contest_id);
            
            if(contest == null){
                throw new NotFoundException("Contest doesn't found", command.NewQuestions.ContestUrl);
            }


            List<QuestionEntity> questionList = command.NewQuestions.Questions.Select((question)=>
            {
                var questionEntity = new QuestionEntity
                {
                    GlobalQuestionUrl = question.Url,
                    Index = question.Index,
                    ContestId = contest.Id
                };
                return questionEntity;
            })
            .ToList();

            await _unitOfWork.QuestionRepository.CreateListAsync(questionList);
            return true;
        }
        public static string ParseIdFromUrl(string url)
        {
            url = Uri.UnescapeDataString(url);
            int index = url.LastIndexOf('/');
            string id = url.Substring(index + 1);

            return id;
        }
    }
}
