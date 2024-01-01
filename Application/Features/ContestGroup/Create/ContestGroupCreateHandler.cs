using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.DTOs.ContestGroup;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContestGroup.Create;

public class ContestGroupCreateHandler : IRequestHandler<ContestGroupCreateCommand, ContestGroupResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ContestGroupCreateHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContestGroupResponseDto> Handle(ContestGroupCreateCommand request, CancellationToken cancellationToken)
    {
        var contestGroup = _mapper.Map<ContestGroupEntity>(request.ContestGroupRequestDto);
        // var result = await _unitOfWork.ContestGroupRepository.CreateAsync(contestGroup);

        return _mapper.Map<ContestGroupResponseDto>(null);
    }
}