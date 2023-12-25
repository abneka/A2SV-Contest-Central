using Application.Contracts.Persistence;
using Application.DTOs.Group;
using Application.Exceptions;
using Application.Features.Group.Commands.CreateGroup;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Group.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GroupResponseDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateGroupCommandValidator();
        var validationResult = validator.Validate(request);
        
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);
        var toBeUpdatedGroup = await _unitOfWork.A2SVGroupRepository.GetByIdAsync(request.Id);
        if (toBeUpdatedGroup == null)
            throw new NotFoundException(nameof(toBeUpdatedGroup), request.Id);
        var group = _mapper.Map(request.GroupDto, toBeUpdatedGroup);
        var result = await _unitOfWork.A2SVGroupRepository.UpdateAsync(toBeUpdatedGroup.Id, group);

        return _mapper.Map<GroupResponseDto>(result);
    }
}