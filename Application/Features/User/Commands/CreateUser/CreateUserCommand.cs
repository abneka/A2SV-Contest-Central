using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public UserRequestDto UserDto { get; set; } = null!;
}