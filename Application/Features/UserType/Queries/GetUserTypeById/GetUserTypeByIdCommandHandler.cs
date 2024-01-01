using Application.Contracts.Persistence;
using Application.DTOs.User;
using Application.Features.Location.Queries.GetLocationById;
using Application.Features.User.Queries.GetSingleUser;
using AutoMapper;
using MediatR;

namespace Application.Features.UserType.Queries.GetUserTypeById;

public class GetUserTypeByIdCommandHandler : IRequestHandler<GetUserTypeByIdQuery, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserTypeByIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserResponseDto> Handle(GetUserTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserTypeRepository.GetByIdAsync(request.Id);
        return _mapper.Map<UserResponseDto>(user);
    }
}