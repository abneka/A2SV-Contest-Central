using Application.Contracts.Persistence;
using Application.DTOs.UserType;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserType.Commands.CreateUserType;

public class CreateUserTypeCommandHandler : IRequestHandler<CreateUserTypeCommand, UserTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserTypeDto> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var userType = _mapper.Map<UserTypeEntity>(request.UserTypeDto);
        var created = await _unitOfWork.UserTypeRepository.CreateAsync(userType);
        return _mapper.Map<UserTypeDto>(created);
    }
}