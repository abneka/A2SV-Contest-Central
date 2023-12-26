using Application.Contracts.Persistence;
using Application.DTOs.UserContestResult;
using AutoMapper;
using MediatR;

namespace Application.Features.UserContestResult.Queries.GetAllUserContestResults;

public class GetAllUserContestResultsRequestHandler : IRequestHandler<GetAllUserContestResultsRequest, List<UserContestResultResponseDto>>
{
    private readonly IUserContestResultRepository _userContestResultRepository;
    private readonly IMapper _mapper;

    public GetAllUserContestResultsRequestHandler(IUserContestResultRepository userContestResultRepository, IMapper mapper)
    {
        _userContestResultRepository = userContestResultRepository;
        _mapper = mapper;
    }

    public async Task<List<UserContestResultResponseDto>> Handle(GetAllUserContestResultsRequest request, CancellationToken cancellationToken)
    {
        var userContestResults = await _userContestResultRepository.GetAllAsync();
        
        return _mapper.Map<List<UserContestResultResponseDto>>(userContestResults);
    }
}