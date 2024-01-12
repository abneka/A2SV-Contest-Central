using Application.DTOs.Common;
using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Queries;

public class GetContestLeaderboardRequest : IRequest<ContestWithGraphsDto>
{
    public Guid ContestId { get; set; }
    public FilterRequestDto Filter { get; set; } = null!;
}