using Application.Contracts.Persistence;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validation = new DeleteUserCommandValidator(_userRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _userRepository.GetByIdAsync(request.UserID);
        await _userRepository.DeleteAsync(user!.Id);
        return Unit.Value;
    }
}