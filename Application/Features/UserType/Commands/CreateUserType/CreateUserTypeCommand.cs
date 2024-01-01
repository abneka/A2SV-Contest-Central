using Application.DTOs.UserType;
using MediatR;

namespace Application.Features.UserType.Commands.CreateUserType;
 
public class CreateUserTypeCommand : IRequest<UserTypeDto>
{
    public UserTypeDto UserTypeDto { get; set; } = null!;
}