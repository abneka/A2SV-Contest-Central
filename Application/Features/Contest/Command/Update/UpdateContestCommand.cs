
using MediatR;
using Application.DTOs.Contest;

namespace Application.Features.Contest.Commands.UpdateContest;

public class UpdateContestCommand : IRequest<Unit>
{
    public Guid ContestId {get; set;}
    public ContestRequestDto UpdateContest {get; set;} = null!;
}
