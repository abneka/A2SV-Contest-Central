using Application.DTOs.UserContestResult;
using MediatR;

namespace Application.Features.UserContestResult.Queries.GetUserContestResultByUserId;

public class GetUserContestResultByUserId : IRequest<UserContestResultResponseDto>
{
    public Guid UserId { get; set; }
    public Guid ContestId { get; set; }
}