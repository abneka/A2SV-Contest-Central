using Application.Contracts.Persistence;
using Application.DTOs.Team;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Team.Queries.GetOneTeam;

public class GetOneTeamQueryHandler : IRequestHandler<GetOneTeamQuery, TeamResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOneTeamQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamResponseDto> Handle(GetOneTeamQuery request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdAsync(request.Id);
        
        if (team == null)
        {
            throw new NotFoundException(nameof(Team), request.Id);
        }
        
        return _mapper.Map<TeamResponseDto>(team);
    }
}