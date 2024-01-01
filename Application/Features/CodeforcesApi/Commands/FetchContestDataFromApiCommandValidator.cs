using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using FluentValidation;
using System.Threading.Tasks;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommandValidator
        : AbstractValidator<FetchContestDataFromApiCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeforcesApiService _codeforcesApiService;

        public FetchContestDataFromApiCommandValidator(
            IUnitOfWork unitOfWork,
            ICodeforcesApiService codeforcesApiService
        )
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;

            RuleFor(request => request.ContestId)
                .NotEmpty()
                .WithMessage("Contest Id cannot be empty.")
                .MustAsync(BeValidContestId)
                .WithMessage("Contest not found.")
                .MustAsync(IsContestFetched)
                .WithMessage("Contest already fetched.");
        }

        private async Task<bool> BeValidContestId(string contest_id, CancellationToken cancellationToken)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(contest_id);
            return contest != null;
        }

        private async Task<bool> IsContestFetched(string contest_id, CancellationToken cancellationToken)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(contest_id);
            return contest.Status == "FINISHED";
        }
    }
}

