
using MediatR;
using AutoMapper;
using Application.DTOs.Contest;
using Application.Exceptions;
using Application.Contracts.Persistence;

namespace Application.Features.Contest.Queries.GetSingleContest;

public class GetSingleContestRequestHandler : IRequestHandler<GetSingleContestRequest,ContestResponseDto>
{
    public readonly IMapper _mapper;
    public readonly IContestRepository _contestRepository;
    public GetSingleContestRequestHandler(IContestRepository contestRepository, IMapper mapper)
    {
        _mapper = mapper;
        _contestRepository = contestRepository;
    }

    public async Task<ContestResponseDto> Handle(GetSingleContestRequest request, CancellationToken cancellationToken)
    {
        bool res = await _contestRepository.Exists(request.ContestId);
        if(res == false)
             throw new NotFoundException($"Contest with id {request.ContestId} does't exist!", request);

        var contest =  await _contestRepository.GetByIdAsync(request.ContestId);
        return _mapper.Map<ContestResponseDto>(contest);
    }
}