using Application.Contracts.Infrastructure.ExternalServices;
using MediatR;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommandHandler : IRequestHandler<FetchContestDataFromApiCommand, string>
    {
        ICodeforcesApiService _codeforcesApiService;
        public FetchContestDataFromApiCommandHandler(ICodeforcesApiService codeforcesApiService)
        {
            _codeforcesApiService = codeforcesApiService;
        }
        public async Task<string> Handle(FetchContestDataFromApiCommand command, CancellationToken cancellationToken)
        {
            return await _codeforcesApiService.GetContestData(command.ContestId);
        }
    }
}