
using MediatR;
using Application.DTOs.Contest;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommand : IRequest<ContestResponseDto>
{
    public ContestRequestDto NewContest {get; set;} = null!;
}
