using Application.Features.CodeforcesApi.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> FetchContest(string contest_id)
        {
            string res = await _mediator.Send(new FetchContestDataFromApiCommand{ContestId = contest_id});
            var data = JsonConvert.DeserializeObject(res);
            return Json(data);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
