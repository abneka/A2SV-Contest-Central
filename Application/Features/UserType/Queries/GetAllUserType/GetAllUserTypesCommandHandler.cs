using Application.Contracts.Persistence;
using Application.DTOs.User;
using Application.DTOs.UserType;
using Application.Features.User.Queries.GetAllUsers;
using AutoMapper;
using MediatR;

namespace Application.Features.UserType.Queries.GetAllUserType;

public class GetAllUserTypesCommandHandler : IRequestHandler<GetAllUserTypesCommand, List<UserTypeResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUserTypesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<UserTypeResponseDto>> Handle(GetAllUserTypesCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserTypeRepository.GetAllAsync();
        return _mapper.Map<List<UserTypeResponseDto>>(users);
    }
}