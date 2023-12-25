using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Queries.GetOneGroup;

public class GetOneGroupQuery : IRequest<GroupResponseDto>
{
    public Guid Id { get; set; }
}