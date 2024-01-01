using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var validator = new FetchContestDataFromApiCommandValidator(
                _unitOfWork,
                _codeforcesApiService
            );
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                string contest_id = command.ContestId;

                //fetch data from codeforces using codeforces api
                dynamic data = await _codeforcesApiService.GetContestData(contest_id);

                //status and phase
                if (data.status == "FAILED")
                    return false;
                
                // JObject? contestObject = data.result.contest as JObject;
                //
                // var updated_contest = contestObject?.ToObject<ContestEntity>();
                JObject contestObject = data.result.contest;
                Console.WriteLine("here: ");
                Console.WriteLine(data.GetType());
                Console.WriteLine(data.result.contest);

                var updated_contest = new ContestEntity
                {
                    Type = contestObject["type"]?.ToString() ?? "",
                    DurationSeconds = contestObject["durationSeconds"]?.ToObject<int>() ?? default(int),
                    StartTimeSeconds = contestObject["startTimeSeconds"]?.ToObject<int>() ?? default(int),
                    RelativeTimeSeconds = contestObject["relativeTimeSeconds"]?.ToObject<int>() ?? default(int),
                    PreparedBy = contestObject["preparedBy"]?.ToString() ?? "",
                    WebsiteUrl = contestObject["websiteUrl"]?.ToString() ?? "",
                    // Description = contestObject["description"]?.ToString() ?? "",
                    Difficulty = contestObject["difficulty"]?.ToObject<string>() ?? "",
                    Kind = contestObject["kind"]?.ToString() ?? "",
                    Status = contestObject["phase"]?.ToString() ?? "",
                    Season = contestObject["season"]?.ToString() ?? ""
                };
                
                Console.WriteLine("new UPDATED!!!!");
                Console.WriteLine(updated_contest);
                
                // var updated_contest = new ContestEntity
                // {
                //     Type = data.result.contest.type ?? "",
                //     DurationSeconds = data.result.contest.durationSeconds ?? default(int),
                //     StartTimeSeconds = data.result.contest.startTimeSeconds ?? default(int),
                //     RelativeTimeSeconds = data.result.contest.relativeTimeSeconds ?? default(int),
                //     PreparedBy = data.result.contest.preparedBy ?? "",
                //     WebsiteUrl = data.result.contest.websiteUrl ?? "",
                //     // Description = data.result.contest.description ?? "",
                //     Difficulty = data.result.contest.difficulty ?? default(int),
                //     Kind = data.result.contest.kind ?? "",
                //     Status = data.result.contest.phase ?? "",
                //     Season = data.result.contest.season ?? ""
                // };

                if (updated_contest != null)
                    await _unitOfWork.ContestRepository.UpdateContestByGlobalIdAsync(
                        contest_id,
                        updated_contest
                );

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
