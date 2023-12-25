using Application.DTOs.Location;
using MediatR;

namespace Application.Features.Location.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest<Unit>
{
    public LocationDto LocationDto { get; set; } = null!;
    public Guid Id { get; set; }
}