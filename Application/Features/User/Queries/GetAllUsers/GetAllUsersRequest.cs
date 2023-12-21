using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersRequest : IRequest<List<UserResponseDto>>
{
    
}