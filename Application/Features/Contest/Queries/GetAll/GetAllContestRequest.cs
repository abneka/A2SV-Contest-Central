using Application.DTOs.Common;
using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Queries.GetAll;

public class GetAllContestsRequest : IRequest<PaginatedResult<ContestResponseDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}