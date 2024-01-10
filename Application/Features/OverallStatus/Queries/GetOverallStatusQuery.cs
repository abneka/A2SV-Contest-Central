using Application.DTOs.Common;
using Application.DTOs.OverallStatus;
using MediatR;

namespace Application.Features.OverallStatus.Queries;

public class GetOverallStatusQuery : IRequest<OverallStatusResponseDto>
{
    public OverallStatusRequestDto Filter { get; set; }
}