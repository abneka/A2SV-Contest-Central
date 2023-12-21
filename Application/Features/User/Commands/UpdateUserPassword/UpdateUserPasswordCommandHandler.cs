using Application.Contracts.Persistence;
using Application.Contracts.Infrastructure;
using AutoMapper;
using Domain.Entities.Email;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    
    public UpdateUserPasswordCommandHandler(IMapper mapper, IUserRepository userRepository, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }
    
    public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserPasswordCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _userRepository.GetUserByEmail(request.AuthRequest.Email);
        user!.Password = BCrypt.Net.BCrypt.HashPassword(request.AuthRequest.Password);

        var http = new HttpContextAccessor();
        var scheme = http.HttpContext?.Request.Scheme?? "https";
        var host = http.HttpContext?.Request.Host.Value?? "localhost:44322";
        
        await _emailSender.SendEmail(new Email()
        {
            To = request.AuthRequest.Email,
            Subject = "Social Media App Security Update",
            Body = $"Your password has been updated. If you did not make this change, please contact us immediately at <a href='mailto:" + "nekahiwota@gmail.com" + "'>email</a>"
        });

        await _userRepository.UpdateAsync(user.Id, user);
        return Unit.Value;
    }
}