using Application.Contracts.Persistence;
using Application.DTOs.User;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetAllUsers;

public class GetAllUserRequestHandler : IRequestHandler<GetAllUsersRequest, List<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetAllUserRequestHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserResponseDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserResponseDto>>(users);
    }
}