using Application.DTOs.Common;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Queries.GetUsersByFiltration;

public class GetUsersByFiltrationQuery : IRequest<PaginatedUserResponseDto>
{
    public FilterUserRequestDto Filter { get; set; } = null!;
}