using MediatR;
using Application.DTOs.Contest;

namespace Application.Features.Contest.Queries.GetAllContests;

public class GetAllContestsRequest : IRequest<IReadOnlyList<ContestResponseDto>>
{

}