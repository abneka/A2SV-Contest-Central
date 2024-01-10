using Application.DTOs.TeamContestResult;
using Application.Features.TeamContestResult.Queries.GetAllTeamContestResults;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("AllowAnyOrigin")]
[Route("api/[controller]")]
public class TeamContestResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamContestResultController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // get all team's contest results
    [HttpGet("all")]
    public async Task<ActionResult<List<TeamContestResultResponseDto>>> Get()
    {
        return await _mediator.Send(new GetAllTeamContestResultsQuery());
    }
}