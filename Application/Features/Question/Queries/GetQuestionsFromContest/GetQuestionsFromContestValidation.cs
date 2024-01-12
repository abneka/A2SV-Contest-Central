using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Question.Queries.GetQuestionsFromContest;

public class GetQuestionsFromContestValidation : AbstractValidator<GetQuestionsFromContestRequest>
{
    public GetQuestionsFromContestValidation(IUnitOfWork unitOfWork)
    {
        RuleFor(req => req.ContestId)
            .NotEmpty()
            .WithMessage("Contest URL cannot be empty.")
            .MustAsync(async (contestId, token) => await unitOfWork.ContestRepository.Exists(contestId))
            .WithMessage("Invalid Codeforces contest URL.");
    }
}