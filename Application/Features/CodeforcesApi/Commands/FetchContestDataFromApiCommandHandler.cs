using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Application.Models;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommandHandler
        : IRequestHandler<FetchContestDataFromApiCommand, Unit>
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

        public async Task<Unit> Handle(
            FetchContestDataFromApiCommand command,
            CancellationToken cancellationToken
        )
        {
            string contest_id = command.ContestId;

            try
            {
                var old_contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(contest_id);
                //first create contest
                if(old_contest == null)
                    return Unit.Value;

                //fetch data from codeforces using codeforces api
                dynamic data = await _codeforcesApiService.GetContestData(contest_id);
                
                //status and phase
                if (data.status == "FAILED")
                    return Unit.Value;
                
                //if contest already fetched
                if(old_contest.Status == "FINISHED"){
                    return Unit.Value;
                }

                var contestData = data.result.contest;
                var update_contest = new ContestEntity
                {
                    Type = contestData.type,
                    DurationSeconds = contestData.durationSeconds,
                    StartTimeSeconds = contestData.startTimeSeconds,
                    RelativeTimeSeconds = contestData.relativeTimeSeconds,
                    PreparedBy = contestData.preparedBy,
                    WebsiteUrl = contestData.websiteUrl,
                    Description = contestData.description ?? "",
                    Difficulty = contestData.difficulty,
                    Kind = contestData.kind,
                    Status = contestData.phase,
                    Season = contestData.season
                };

                await _unitOfWork.ContestRepository.UpdateContestByGlobalIdAsync(contest_id, update_contest);

                // if contest hasn't completed yet
                if(data.result.contest.phase != "FINISHED"){
                    return Unit.Value;
                }

                // contest info
                // questions info
                // contest result
                // standing
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"An error occurred while fetching data from Codeforces: {ex.Message}"
                );
            }
            return Unit.Value;
        }
    }
}
