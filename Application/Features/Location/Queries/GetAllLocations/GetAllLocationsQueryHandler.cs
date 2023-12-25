using Application.Contracts.Persistence;
using Application.DTOs.Location;
using AutoMapper;
using MediatR;

namespace Application.Features.Location.Queries.GetAllLocations;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, List<LocationResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLocationsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public Task<List<LocationResponseDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = _unitOfWork.LocationRepository.GetAllAsync();
        return _mapper.Map<Task<List<LocationResponseDto>>>(locations);
    }
}