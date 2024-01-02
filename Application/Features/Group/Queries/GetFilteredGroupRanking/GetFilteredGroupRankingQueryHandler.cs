using Application.Contracts.Persistence;
using Application.DTOs.Group;
using AutoMapper;
using MediatR;

namespace Application.Features.Group.Queries.GetFilteredGroupRanking;

public class GetFilteredGroupRankingQueryHandler : IRequestHandler<GetFilteredGroupRankingQuery, List<GroupRankingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFilteredGroupRankingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GroupRankingDto>> Handle(GetFilteredGroupRankingQuery request, CancellationToken cancellationToken)
    {
        // get all groups 
        // get all members of each group and the ids of their completed challenges which will enable us to count the number of completed challenges by each group member and the group too things get easy from here
        return new List<GroupRankingDto>();
    }
}