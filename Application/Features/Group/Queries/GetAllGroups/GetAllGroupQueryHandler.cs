using Application.Contracts.Persistence;
using Application.DTOs.Group;
using AutoMapper;
using MediatR;

namespace Application.Features.Group.Queries.GetAllGroups;

public class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GroupResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGroupQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GroupResponseDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _unitOfWork.A2SVGroupRepository.GetAllAsync();
        return _mapper.Map<List<GroupResponseDto>>(groups);
    }
}