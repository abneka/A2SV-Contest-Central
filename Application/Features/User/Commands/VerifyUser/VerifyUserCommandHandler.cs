using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.VerifyUser;

public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public VerifyUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        var validation = new VerifyUserCommandValidator(_userRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _userRepository.GetUserByEmail(request.Email);
        // user = _mapper.Map<UserEntity>(user);
        if (user?.Token != request.Token)
        {
            throw new Exception("Invalid token");
        }
        else
        {
            user.IsVerified = true;
            user.Token = Guid.NewGuid().ToString();
            await _userRepository.UpdateAsync(user.Id, user);
            return true;
        }
    }
}