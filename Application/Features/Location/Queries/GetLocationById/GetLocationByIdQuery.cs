using Application.DTOs.Location;
using MediatR;

namespace Application.Features.Location.Queries.GetLocationById;

public class GetLocationByIdQuery : IRequest<LocationResponseDto>
{
    public Guid LocationId { get; set; }
}