using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Command.Create;

public class CreateContestCommand : IRequest<ContestResponseDto>
{
    public ContestRequestDto NewContest {get; set;} = null!;
}
