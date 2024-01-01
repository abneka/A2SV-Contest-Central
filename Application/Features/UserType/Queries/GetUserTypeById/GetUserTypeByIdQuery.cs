using Application.DTOs.User;
using MediatR;

namespace Application.Features.UserType.Queries.GetUserTypeById;

public class GetUserTypeByIdQuery : IRequest<UserResponseDto>
{
    public Guid Id { get; set; }
}