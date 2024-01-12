using Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.AddProfilePicture;

public class AddProfilePictureCommand : IRequest<UserResponseDto>
{
    public Guid UserId { get; set; }
    public IFormFile ImageFile { get; set; } = null!;
}