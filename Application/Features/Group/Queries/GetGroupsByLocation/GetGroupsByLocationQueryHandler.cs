using Application.Contracts.Persistence;
using Application.DTOs.Group;
using AutoMapper;
using MediatR;

namespace Application.Features.Group.Queries.GetGroupsByLocation;

public class GetGroupsByLocationQueryHandler : IRequestHandler<GetGroupsByLocationQuery, List<GroupResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGroupsByLocationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GroupResponseDto>> Handle(GetGroupsByLocationQuery request, CancellationToken cancellationToken)
    {
        var groups = await _unitOfWork.A2SVGroupRepository.GetGroupsByLocation(request.LocationId);

        return _mapper.Map<List<GroupResponseDto>>(groups);
    }
}