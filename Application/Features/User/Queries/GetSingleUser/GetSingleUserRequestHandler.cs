using Application.Contracts.Persistence;
using Application.DTOs.User;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetSingleUser;

public class GetSingleUserRequestHandler : IRequestHandler<GetSingleUserRequest, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetSingleUserRequestHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserResponseDto> Handle(GetSingleUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        return _mapper.Map<UserResponseDto>(user);
    }
}