using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommandHandler
        : IRequestHandler<FetchContestDataFromApiCommand, bool>
    {
        private readonly ICodeforcesApiService _codeforcesApiService;
        private readonly IUnitOfWork _unitOfWork;

        public FetchContestDataFromApiCommandHandler(
            IUnitOfWork unitOfWork,
            ICodeforcesApiService codeforcesApiService
        )
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;
        }

        public async Task<bool> Handle(
            FetchContestDataFromApiCommand command,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var validator = new FetchContestDataFromApiCommandValidator(
                    _unitOfWork,
                    _codeforcesApiService
                );
                var validationResult = await validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                string contest_id = command.ContestId;

                //fetch data from codeforces using codeforces api
                dynamic data = await _codeforcesApiService.GetContestData(contest_id);

                //status and phase
                if (data.status == "FAILED")
                    return false;

                // var updated_contest = new ContestEntity
                // {
                //     Type = data.result.contest.type,
                //     DurationSeconds = data.result.contest.durationSeconds,
                //     StartTimeSeconds = data.result.contest.startTimeSeconds,
                //     RelativeTimeSeconds = data.result.contest.relativeTimeSeconds,
                //     PreparedBy = data.result.contest.preparedBy,
                //     WebsiteUrl = data.result.contest.websiteUrl  ?? "",
                //     Description = data.result.contest.description ?? "",
                //     Difficulty = data.result.contest.difficulty ?? "",
                //     Kind = data.result.contest.kind ?? "",
                //     Status = data.result.contest.phase ?? "",
                //     Season = data.result.contest.season ?? ""
                // };

                // await _unitOfWork.ContestRepository.UpdateContestByGlobalIdAsync(
                //     contest_id,
                //     updated_contest
                // );

                // if contest hasn't completed yet
                if (data.result.contest.phase == "CODING")
                {
                    return false;
                }

                // questions info
                // contest result
                // standing
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching data from Codeforces", ex);
                throw new Exception("An error occurred while fetching data from Codeforces");
            }
            
        }
    }
}

