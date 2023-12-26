using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Features.Auth.ForgetPassword;
using Application.Features.Auth.LogIn;
using Application.Features.Auth.ResetPassword;
using Application.Features.User.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(UserRequestDto userDto)
    {
        if (userDto == null)
        {
            throw new Exception("");
        }
        var response = await _mediator.Send(new CreateUserCommand
        {
            UserDto = userDto
        });
        return response;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest)
    {
        var response = await _mediator.Send(new LoginUserCommand
        {
            AuthRequest = authRequest
        });
        return response;
    }
    
    [HttpPost("forgot_password")]
    public async Task<ActionResult<Unit>> ForgotPassword(string email)
    {
        var response = await _mediator.Send(new ForgetPasswordCommand
        {
            Email = email
        });
        return response;
    }
    
    [HttpPost("reset_password")]
    public async Task<ActionResult<string>> ResetPassword(ForgetPasswordRequest forgetPasswordRequest)
    {
        var response = await _mediator.Send(new ResetPasswordCommand
        {
            ForgetPasswordRequest = forgetPasswordRequest
        });
        return response;
    }
}