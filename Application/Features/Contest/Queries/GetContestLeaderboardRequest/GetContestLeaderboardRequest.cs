using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Queries;

public class GetContestLeaderboardRequest : IRequest<List<UserContestAndQuestionDto>>
{
    public Guid ContestId { get; set; }
}