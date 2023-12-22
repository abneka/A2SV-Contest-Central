using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<GroupResponseDto>
{
    public GroupRequestDto GroupRequestDto { get; set; } = null!;
}