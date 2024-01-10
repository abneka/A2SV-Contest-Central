using System.Text;
using Application.Contracts.Persistence;
using Application.Contracts.Infrastructure;
using AutoMapper;
using Domain.Entities.Email;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.ForgetPassword;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;

    public ForgetPasswordCommandHandler(IMapper mapper, IEmailSender emailSender, IUserRepository userRepository)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _userRepository = userRepository;
    }
    
    public async Task<Unit> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new ForgetPasswordCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _userRepository.GetUserByEmail(request.Email);
        user!.ConfirmationCode = GenerateConfirmationCode(6);
        user.ConfirmationCodeExpiration = DateTime.UtcNow.AddMinutes(30);
        await _userRepository.UpdateAsync(user.Id, user);
        
        var http = new HttpContextAccessor();
        var scheme = http.HttpContext?.Request.Scheme?? "https";
        var host = http.HttpContext?.Request.Host.Value?? "localhost:44322";

        await _emailSender.SendEmail(new Email()
        {
            To = request.Email,
            Subject = "A2SV Contest Central Reset Password",
            Body =
                $"This User requested to reset password, if you are not the one who requested this, please ignore this email. <br/> Please reset your password by using the confirmation code below: <br/> {user.ConfirmationCode} <br/> this code will expire in 30 minutes."
        });
        return Unit.Value;
    }
    
    static string GenerateConfirmationCode(int length)
    {
        var random = new Random();
        var codeBuilder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            codeBuilder.Append(random.Next(10)); // Generates a random digit (0-9)
        }

        return codeBuilder.ToString();
    }
}