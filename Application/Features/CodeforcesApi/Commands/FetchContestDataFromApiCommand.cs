using MediatR;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommand : IRequest<bool>
    {
        public string ContestId { get; set; } = null!;
    }
}