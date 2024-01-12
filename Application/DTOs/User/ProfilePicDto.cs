using Microsoft.AspNetCore.Http;

namespace Application.DTOs.User;

public class ProfilePicDto
{
    public IFormFile ImageFile { get; set; } = null!;
}