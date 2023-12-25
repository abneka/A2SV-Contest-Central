using Application.DTOs.Group;
using MediatR;

namespace Application.Features.Group.Queries.GetAllGroups;

public class GetAllGroupsQuery : IRequest<List<GroupResponseDto>>
{
    
}