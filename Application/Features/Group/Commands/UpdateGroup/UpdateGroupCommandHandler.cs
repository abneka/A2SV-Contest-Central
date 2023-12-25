using Application.Contracts.Persistence;
using Application.DTOs.Group;
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
        
        var group = _mapper.Map<GroupEntity>(request.GroupDto);
        var result = await _unitOfWork.A2SVGroupRepository.UpdateAsync(group.Id, group);

        return _mapper.Map<GroupResponseDto>(result);
    }
}