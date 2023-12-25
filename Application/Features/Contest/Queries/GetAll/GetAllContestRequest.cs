using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Queries.GetAll;

public class GetAllContestsRequest : IRequest<IReadOnlyList<ContestResponseDto>>
{

}