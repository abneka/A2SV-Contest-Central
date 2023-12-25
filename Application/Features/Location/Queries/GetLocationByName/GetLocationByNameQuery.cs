using Application.DTOs.Location;
using MediatR;

namespace Application.Features.Location.Queries.GetLocationByName;

public class GetLocationsByNameQuery : IRequest<List<LocationResponseDto>>
{
    
}