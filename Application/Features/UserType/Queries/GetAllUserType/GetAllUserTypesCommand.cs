using Application.DTOs.User;
using Application.DTOs.UserType;
using MediatR;

namespace Application.Features.UserType.Queries.GetAllUserType;

public class GetAllUserTypesCommand : IRequest<List<UserTypeResponseDto>>
{
    
}