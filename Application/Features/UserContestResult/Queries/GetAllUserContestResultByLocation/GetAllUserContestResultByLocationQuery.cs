using Application.DTOs.UserContestResult;
using MediatR;

namespace Application.Features.UserContestResult.Queries.GetAllUserContestResultByLocation;

public class GetAllUserContestResultByLocationQuery : IRequest<UserContestResultResponseDto>
{
    public Guid LocationId { get; set; }
}