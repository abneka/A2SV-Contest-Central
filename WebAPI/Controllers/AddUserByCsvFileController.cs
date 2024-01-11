using Application.Features.User.Commands.AddUserUsingCsvFile;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddUserByCsvFileController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AddUserByCsvFileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("UploadCsvFile")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            await _mediator.Send(new AddUserUsingCsvFileCommand { UserFile = file });

            return Ok("File uploaded successfully");
        }  
    }
}