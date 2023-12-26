using Application.Contracts.Persistence;
using Application.DTOs.Group;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GroupResponseDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroupCommandValidator();
        var validationResult = validator.Validate(request);
        
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);
        
        var group = _mapper.Map<GroupEntity>(request.GroupDto);
        var result = await _unitOfWork.A2SVGroupRepository.CreateAsync(group);

        return _mapper.Map<GroupResponseDto>(result);
    }
}