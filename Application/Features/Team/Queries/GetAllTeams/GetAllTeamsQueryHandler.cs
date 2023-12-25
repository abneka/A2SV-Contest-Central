using Application.Contracts.Persistence;
using Application.DTOs.Team;
using AutoMapper;
using MediatR;

namespace Application.Features.Team.Queries.GetAllTeams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, List<TeamResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTeamsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TeamResponseDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _unitOfWork.TeamRepository.GetAllAsync();
        return _mapper.Map<List<TeamResponseDto>>(teams);
    }
}