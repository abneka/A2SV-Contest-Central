using Application.Contracts.Persistence;
using Application.Contracts.Infrastructure;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Email;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public CreateUserCommandHandler(IUserRepository userRepository, IEmailSender emailSender, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = _mapper.Map<UserEntity>(request.UserDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
        user.Token = Guid.NewGuid().ToString();
        var res = await _userRepository.CreateAsync(user);
        await _emailSender.SendEmail(new Email()
        {
            To = request.UserDto.Email
        });
        var http = new HttpContextAccessor();
        var scheme = http.HttpContext?.Request.Scheme?? "https";
        var host = http.HttpContext?.Request.Host.Value?? "localhost:44322";
        
        await _emailSender.SendEmail(new Email()
        {
            To = request.UserDto.Email,
            Subject = "Social Media App Verification",
            Body = $"Please verify your account by clicking the link below: <br/> <a href='{scheme}://{host}/Users/verify?email={user.Email.Replace("@", "%40")}&token={user.Token}'>Verify Email</a>"
        });
        return res.Id;
    }
}