using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Commands.UpdateContest;

public class UpdateContestCommand : IRequest<Unit>
{
    public Guid ContestId { get; set; }
    public ContestInfoRequestDto UpdateContest { get; set; } = null!;
}
