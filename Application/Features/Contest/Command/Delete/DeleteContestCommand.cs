
using MediatR;
using Application.DTOs.Contest;

namespace Application.Features.Contest.Commands.DeleteContest;

public class DeleteContestCommand : IRequest<Unit>
{
    public Guid ContestId {get; set;}
}
