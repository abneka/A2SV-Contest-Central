using Application.DTOs.Common;
using Application.DTOs.Contest;
using MediatR;

namespace Application.Features.Contest.Queries.GetContestsByFiltration;

public class GetContestsByFiltrationQuery : IRequest<PaginatedContestResponseDto>
{
    public FilterRequestDto Filter { get; set; } = null!;
}