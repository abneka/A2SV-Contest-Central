using Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.AddUserUsingCsvFile
{
    public class AddUserUsingCsvFileCommand : IRequest<IReadOnlyList<InvalidUserRecord>>
    {
        public IFormFile UserFile { get; set; } = null!;
    }
}


