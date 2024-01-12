using Application.DTOs.Common;
using Application.DTOs.Contest;
using Application.DTOs.Contest.CodeforcesExtension;
using Application.DTOs.Question;
using Application.Features.Contest.Command.Create;
using Application.Features.Contest.Command.CreateOrUpdateContestByExtension;
using Application.Features.Contest.Commands.DeleteContest;
using Application.Features.Contest.Commands.UpdateContest;
using Application.Features.Contest.Queries;
using Application.Features.Contest.Queries.GetContestsByFiltration;
using Application.Features.Contest.Queries.GetSingleContest;
using Application.Features.Question.Commands.CreateOrUpdate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        [Route("GetSingleContest/{contestId:guid}")]
        public async Task<ActionResult<ContestResponseDto>> GetSingleContest(Guid contestId)
        {
            var contest = await _mediator.Send(
                new GetSingleContestRequest { ContestId = contestId }
            );
            return Ok(contest);
        }

        [HttpPost]
        [Route("CreateContest")]
        public async Task<ActionResult<ContestResponseDto>> CreateContest(ContestRequestDto contestRequest)
        {

            var new_contest = new ContestInfoRequestDto{
                ContestName = contestRequest.ContestName,
                ContestUrl = contestRequest.ContestUrl
            };

            var command = new CreateContestCommand { NewContest = new_contest };
            var contest = await _mediator.Send(command);
            
            var new_questions = new QuestionRequestDto{
                ContestId = contest.Id,
                Questions = contestRequest.Questions
            };

            await _mediator.Send(new CreateOrUpdateQuestionCommand{ NewQuestions = new_questions});

            return Ok("created");
            // return CreatedAtAction(nameof(GetSingleContest), new{Id = contest.Id}, contest);
        }

        [HttpPost]
        [Route("CreateContestByExtension")]
        public async Task<ActionResult<ContestExtResponseDto>> CreateContestByExtension(
            ContestInfoRequestDto contestRequest
        )
        {
            
            var command = new CreateOrUpdateContestByExtensionCommand
            {
                NewContest = contestRequest
            };
            var contest = await _mediator.Send(command);

            return Ok(contest);
        }

        [HttpPut]
        [Route("UpdateContest/{id}")]
        public async Task<ActionResult> UpdateContest(Guid id, ContestInfoRequestDto ContestRequest)
        {
            var command = new UpdateContestCommand
            {
                ContestId = id,
                UpdateContest = ContestRequest
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteContest/{id}")]
        public async Task<ActionResult> DeleteContest(Guid id)
        {
            await _mediator.Send(new DeleteContestCommand { ContestId = id });
            return NoContent();
        }

        [HttpGet]
        [Route("GetContestsByFilter")]
        public async Task<ActionResult<PaginatedContestResponseDto>> GetContestsByFiltration(
            [FromQuery] FilterRequestDto query
        )
        {
            return await _mediator.Send(new GetContestsByFiltrationQuery { Filter = query });
        }

        [HttpGet]
        [Route("GetContestLeaderboardWithGraph/{contestId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ContestWithGraphsDto>>> GetContestLeaderboard(Guid contestId, [FromQuery] FilterRequestDto filter)
        {
            var contestLeaderboard = await _mediator.Send(new GetContestLeaderboardRequest{ ContestId = contestId, Filter = filter });
            return Ok(contestLeaderboard);
        }

        // [HttpGet]
        // [Route("GetContestResultByUserId/{UserId:int}")]
        // public async Task<ActionResult<IReadOnlyList<ContestResponseDto>>> GetContestsByUserId(Guid userId)
        // {
        //     var contests = await _mediator.Send(new GetAllContestsByUserIdRequest{UserId = userId});
        //     return Ok(contests);
        // }

        // [HttpGet]
        // [Route("SearchContest/{query}")]
        // public async Task<ActionResult<IReadOnlyList<ContestResponseDto>>> SearchContest(string query)
        // {
        //    var contests = await _mediator.Send(new SearchContestRequest {Query = query});
        //    return Ok(contests);
        // }
    }
}

