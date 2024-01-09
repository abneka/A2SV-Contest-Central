using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using AutoMapper;
using MediatR;

namespace Application.Features.Contest.Queries;

public class GetContestLeaderboardHandler : IRequestHandler<GetContestLeaderboardRequest, List<UserContestAndQuestionDto>>
{
    private readonly IContestRepository _contestRepository;
    private readonly IMapper _mapper;

    public GetContestLeaderboardHandler(IContestRepository contestRepository, IMapper mapper)
    {
        _contestRepository = contestRepository;
        _mapper = mapper;
    }

    public async Task<List<UserContestAndQuestionDto>> Handle(GetContestLeaderboardRequest request, CancellationToken cancellationToken)
    {
        return await _contestRepository.GetContestLeaderboard(request.ContestId);
    }
}