using Application.DTOs.Team;
using MediatR;

namespace Application.Features.Team.Queries.GetAllTeams;

public class GetAllTeamsQuery : IRequest<List<TeamResponseDto>>
{ 
    
}