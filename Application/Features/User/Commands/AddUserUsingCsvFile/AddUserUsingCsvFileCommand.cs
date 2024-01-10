using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.AddUserUsingCsvFile
{
    public class AddUserUsingCsvFileCommand : IRequest<Unit>
    {
        public IFormFile UserFile { get; set; } = null!;
    }
}


