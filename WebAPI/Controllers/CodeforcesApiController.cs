using Application.Features.CodeforcesApi.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class CodeforcesApiController : Controller
    {
        private readonly IMediator _mediator;

        public CodeforcesApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("fetch/{contest_id}")]
        public async Task<IActionResult> FetchContest(Guid contest_id)
        {
            await _mediator.Send(new FetchContestDataFromApiCommand { ContestId = contest_id });
            return Ok();
        }

    }
}
