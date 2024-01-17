using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Models.Authentication;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.LogIn;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IAuthenticationService _authService;
    private readonly IUnitOfWork _unitOfWork;
    
    public LoginUserCommandHandler(IAuthenticationService authService, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginUserCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetUserByEmail(request.AuthRequest.Email);
        if(user == null){
            throw new NotFoundException("Invalid email or password", request.AuthRequest.Email);
        }

        var role = await _unitOfWork.UserTypeRepository.GetByIdAsync(user.UserTypeId);

        var login_request = new LoginRequest{
            Email = request.AuthRequest.Email,
            LoginPassword = request.AuthRequest.Password,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            OriginalPassword = user.Password,
            Role = role?.Name ?? string.Empty,
            Priority = role?.Priority ?? 0
        };

        var res = _authService.Login(login_request);
        return res;
    }

}