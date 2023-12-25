using Application.Contracts.Persistence;
using Application.DTOs.Location;
using Application.Features.Location.Queries.GetAllLocations;
using AutoMapper;
using MediatR;

namespace Application.Features.Location.Queries.GetLocationById;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLocationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public Task<LocationResponseDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = _unitOfWork.LocationRepository.GetByIdAsync(request.LocationId);
        return _mapper.Map<Task<LocationResponseDto>>(location);
    }
}