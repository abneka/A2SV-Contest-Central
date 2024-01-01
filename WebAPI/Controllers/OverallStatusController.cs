using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public class OverallStatusController : ControllerBase
{
    [HttpGet]
    [Route("api/overall-status")]
    // object of totalContest, totalGroups, totalQuestions, totalMembers, totalHours for the current year
    public async Task<IActionResult> GetOverallStatus()
    {
        return Ok(await GetOverallStatus());
    }
}