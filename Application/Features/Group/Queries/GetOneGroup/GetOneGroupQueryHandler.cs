using Application.Contracts.Persistence;
using Application.DTOs.Group;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Group.Queries.GetOneGroup;

public class GetOneGroupQueryHandler : IRequestHandler<GetOneGroupQuery, GroupResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOneGroupQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GroupResponseDto> Handle(GetOneGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _unitOfWork.A2SVGroupRepository.GetByIdAsync(request.Id);
        
        if (group is null)
        {
            throw new NotFoundException(nameof(Group), request.Id);
        }
        
        return _mapper.Map<GroupResponseDto>(group);
    }
}