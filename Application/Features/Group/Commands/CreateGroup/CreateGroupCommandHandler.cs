using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupResponseDto>
{
    public Task<GroupResponseDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}