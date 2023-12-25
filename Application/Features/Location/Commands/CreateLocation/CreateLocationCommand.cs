using Application.DTOs.Location;
using MediatR;

namespace Application.Features.Location.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<LocationResponseDto>
{
    public LocationDto LocationDto { get; set; } = null!;
}