using Application.DTOs.UserType;
using Application.Features.UserType.Commands.CreateUserType;
using Application.Features.UserType.Queries.GetAllUserType;
using Application.Features.UserType.Queries.GetUserTypeById;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<List<UserTypeResponseDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllUserTypesCommand());
        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetById/{id:guid}")]
    public async Task<ActionResult<UserTypeResponseDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetUserTypeByIdQuery
        {
          Id  = id
        });
        return Ok(result);
    }
    
    [HttpPost]
    [Route("CreateUserType")]
    public async Task<ActionResult<UserTypeResponseDto>> Create(UserTypeDto dto)
    {
        var result = await _mediator.Send(new CreateUserTypeCommand
        {
         UserTypeDto   = dto
        });
        return Ok(result);
    }
    
}