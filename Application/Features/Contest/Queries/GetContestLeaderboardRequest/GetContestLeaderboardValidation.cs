using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Contest.Queries;

public class GetContestLeaderboardValidation : AbstractValidator<GetContestLeaderboardRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetContestLeaderboardValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(req => req.ContestId)
            .NotEmpty()
            .WithMessage("Contest URL cannot be empty.")
            .MustAsync(async (contestId, token) => await _unitOfWork.ContestRepository.Exists(contestId))
            .WithMessage("Invalid Codeforces contest URL.");
    }
}