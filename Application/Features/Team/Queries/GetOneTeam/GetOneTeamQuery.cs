using Application.DTOs.Team;
using MediatR;

namespace Application.Features.Team.Queries;

public class GetOneTeamQuery : IRequest<TeamResponseDto>
{
    public Guid Id { get; set; }
}