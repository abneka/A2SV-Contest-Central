using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Queries.GetSingleUser;

public class GetSingleUserRequest : IRequest<UserResponseDto>
{
    public Guid Id { get; set; }
}