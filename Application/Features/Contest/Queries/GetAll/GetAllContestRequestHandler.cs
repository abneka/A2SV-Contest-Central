
using MediatR;
using AutoMapper;
using Application.DTOs.Contest;
using Application.Contracts.Persistence;
using Application.Features.Contest.Queries.GetAll;

namespace Application.Features.Contest.Queries.GetSingleContest;

public class GetAllContestsRequestHandler : IRequestHandler<GetAllContestsRequest,IReadOnlyList<ContestResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IContestRepository _contestRepository;
    public GetAllContestsRequestHandler(IContestRepository contestRepository, IMapper mapper)
    {
        _mapper = mapper;
        _contestRepository = contestRepository;
    }

    public async Task<IReadOnlyList<ContestResponseDto>> Handle(GetAllContestsRequest request, CancellationToken cancellationToken)
    {
        var contests =  await _contestRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ContestResponseDto>>(contests);
    }
}