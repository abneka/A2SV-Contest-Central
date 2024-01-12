using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Queries.GetGroupsByLocation;

public class GetGroupsByLocationQuery : IRequest<List<GroupResponseDto>>
{
    public Guid LocationId { get; set; }
}