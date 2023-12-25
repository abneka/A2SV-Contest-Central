using Application.DTOs.Location;
using MediatR;

namespace Application.Features.Location.Queries.GetAllLocations;

public class GetAllLocationsQuery : IRequest<List<LocationResponseDto>>
{
    
}