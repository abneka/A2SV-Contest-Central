using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace WebAPI.Controllers;

[ApiController]
public class OverallStatusController : ControllerBase
{
    [HttpGet]
    [Route("overall-status")]
    // object of totalContest, totalGroups, totalQuestions, totalMembers, totalHours for the current year
    public async Task<IActionResult> OverallStatus()
    {
        return Ok(new {temp=20});
    }
}