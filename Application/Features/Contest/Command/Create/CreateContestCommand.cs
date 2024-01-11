using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Command.Create;

public class CreateContestCommand : IRequest<ContestResponseDto>
{
    public ContestInfoRequestDto NewContest { get; set; } = null!;
}
