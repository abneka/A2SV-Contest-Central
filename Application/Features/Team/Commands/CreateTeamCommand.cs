using Application.DTOs.Team;
using Domain.Entities;
using MediatR;

namespace Application.Features.Team.Commands;

public class CreateTeamCommand : IRequest<TeamResponseDto>
{
    public TeamRequestDto Team { get; set; } = null!;
}