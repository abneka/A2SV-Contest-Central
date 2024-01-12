using Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.AddCoverPicture;

public class AddCoverPictureCommand : IRequest<UserResponseDto>
{
    public Guid UserId { get; set; }
    public IFormFile ImageFile { get; set; } = null!;
}