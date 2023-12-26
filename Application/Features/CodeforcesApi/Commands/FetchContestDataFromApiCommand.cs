using MediatR;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommand : IRequest<string>
    {
        public string ContestId { get; set; } = null!;
    }
}