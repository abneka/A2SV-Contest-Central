using Application.DTOs.OverallStatus;
using Application.Features.OverallStatus.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OverallStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public OverallStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("overall-status")]
    // object of totalContest, totalGroups, totalQuestions, totalMembers, totalHours for the current year
    public async Task<IActionResult> OverallStatus([FromQuery] OverallStatusRequestDto filter)
    {
        var result = await _mediator.Send(new GetOverallStatusQuery { Filter = filter });
        return Ok(result);
    }
}