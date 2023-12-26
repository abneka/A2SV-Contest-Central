using Application.DTOs.UserContestResult;
using MediatR;

namespace Application.Features.UserContestResult.Queries.GetAllUserContestResults;

public class GetAllUserContestResultsRequest : IRequest<List<UserContestResultResponseDto>>
{
    
}