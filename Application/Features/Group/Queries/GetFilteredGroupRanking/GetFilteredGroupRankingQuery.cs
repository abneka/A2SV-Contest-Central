using Application.DTOs.Common;
using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Queries.GetFilteredGroupRanking;

public class GetFilteredGroupRankingQuery : IRequest<List<GroupRankingDto>>
{
    public FilterRequestDto FilterRequestDto { get; set; } = null!;
}