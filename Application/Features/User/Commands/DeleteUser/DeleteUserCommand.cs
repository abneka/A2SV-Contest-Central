using MediatR;

namespace Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid UserID { get; set; }
}