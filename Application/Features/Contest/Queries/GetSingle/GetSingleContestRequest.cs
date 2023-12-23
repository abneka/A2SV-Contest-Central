
using MediatR;
using Application.DTOs.Contest;

namespace Application.Features.Contest.Queries.GetSingleContest;

public class GetSingleContestRequest : IRequest<ContestResponseDto>
{
    public Guid ContestId { get; set; }
}