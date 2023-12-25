using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest<GroupResponseDto>
{
    public GroupDto GroupDto { get; set; } = null!;
    public Guid Id { get; set; }
}