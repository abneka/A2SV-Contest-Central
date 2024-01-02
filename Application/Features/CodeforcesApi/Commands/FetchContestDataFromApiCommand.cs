using MediatR;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommand : IRequest<bool>
    {
        public Guid ContestId { get; set; }
    }
}