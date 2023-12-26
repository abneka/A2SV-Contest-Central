using System.Security.Claims;
using Application.DTOs.Contest;
using Application.Features.Contest.Commands.CreateContest;
using Application.Features.Contest.Commands.DeleteContest;
using Application.Features.Contest.Commands.UpdateContest;
using Application.Features.Contest.Queries.GetAllContests;
using Application.Features.Contest.Queries.GetAllContestsByUserId;
using Application.Features.Contest.Queries.GetSingleContest;
using Application.Features.Contest.Queries.SearchContest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using WebApi.Service;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContestsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ContestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ContestResponseDto>>> GetAll()
        {
            var contests = await _mediator.Send(new GetAllContestsRequest());
            return Ok(contests);
        }

        [HttpGet]
        [Route("GetSingleContest/{contestId:int}")]
        public async Task<ActionResult<ContestResponseDto>> GetSingleContest(int contestId)
        {
            var contest = await _mediator.Send(new GetSingleContestRequest{ContestId = contestId});
            return Ok(contest);
        }

        [HttpContest]
        [Route("CreateContest")]
        public async Task<ActionResult<ContestResponseDto>> CreateContest([FromForm]  ContestRequestDto contestRequest)
        {
            var command = new CreateContestCommand
            {
                NewContest = contestRequest
            };
            var contest = await _mediator.Send(command);

            return Ok("created");
            // return CreatedAtAction(nameof(GetSingleContest), new{Id = contest.Id}, contest);
        }

        [HttpPut]
        [Route("UpdateContest/{id:int}")]
        public async Task<ActionResult> UpdateContest(int id, ContestRequestDto ContestRequest)
        {            
            var command = new UpdateContestCommand
            {
                ContestId = id,
                UpdateContest = ContestRequest
            };

            await _mediator.Send(command);
            return NoContent("updated");
        }

        [HttpDelete]
        [Route("DeleteContest/{id:int}")]
        public async Task<ActionResult> DeleteContest(int id)
        {
            await _mediator.Send(new DeleteContestCommand { ContestId = id });
            return NoContent();
        }

        [HttpGet]
        [Route("GetContestResultByUserId/{UserId:int}")]
        public async Task<ActionResult<IReadOnlyList<ContestResponseDto>>> GetContestsByUserId(int userId)
        {
            var contests = await _mediator.Send(new GetAllContestsByUserIdRequest{UserId = userId});
            return Ok(contests);
        }

        [HttpGet]
        [Route("SearchContest/{query}")]
        public async Task<ActionResult<IReadOnlyList<ContestResponseDto>>> SearchContest(string query)
        {
           var contests = await _mediator.Send(new SearchContestRequest {Query = query});
           return Ok(contests);
        }

    }
}