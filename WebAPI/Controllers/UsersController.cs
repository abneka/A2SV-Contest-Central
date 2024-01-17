using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Features.User.Commands.AddCoverPicture;
using Application.Features.User.Commands.AddProfilePicture;
using Application.Features.User.Commands.DeleteUser;
using Application.Features.User.Commands.UpdateUser;
using Application.Features.User.Commands.UpdateUserPassword;
using Application.Features.User.Commands.VerifyUser;
using Application.Features.User.Queries.GetAllUsers;
using Application.Features.User.Queries.GetSingleUser;
using Application.Features.User.Queries.GetUsersByFiltration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service;


namespace WebApi.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> Get(Guid id)
        {
            return await _mediator.Send(new GetSingleUserRequest
            {
                Id = id
            });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersRequest());
            return users;
        }

        [HttpGet("verify")]
        public async Task<ActionResult<bool>> VerifyUser([FromQuery] string email, [FromQuery] string token)
        {
            var response = await _mediator.Send(new VerifyUserCommand
            {
                Email = email,
                Token = token
            });
            return response;
        }
        
        [HttpPut("updatePassword")]
        public async Task<ActionResult<Unit>> UpdatePassword(AuthRequest authRequest)
        {
            await AuthHelper.CheckUserByEmail(User, authRequest.Email);
            var response = await _mediator.Send(new UpdateUserPasswordCommand
            {
                 AuthRequest = authRequest
            });
            return response;
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Unit>> UpdateUser(Guid id, UserUpdateRequestDto userDto)
        {
            await AuthHelper.CheckUserById(User, id);
            
            var response = await _mediator.Send(new UpdateUserCommand
            {
                Id = id,
                UserDto = userDto
            });
            return response;
        }

        [HttpPut]
        [Route("UploadProfilePicture/{userId:guid}")]
        public async Task<UserResponseDto> UploadProfilePicture(Guid userId, [FromForm]ProfilePicDto profilePicDto)
        {
            return await _mediator.Send(new AddProfilePictureCommand
            {
                UserId = userId,
                ImageFile = profilePicDto.ImageFile
            });
        }
        
        [HttpPut]
        [Route("UploadCoverPicture/{userId:guid}")]
        public async Task<UserResponseDto> UploadCoverPicture(Guid userId, [FromForm]ProfilePicDto profilePicDto)
        {
            return await _mediator.Send(new AddCoverPictureCommand
            {
                UserId = userId,
                ImageFile = profilePicDto.ImageFile
            });
        }
        
        [HttpDelete("{id:guid}")]
        public async Task DeleteUser(Guid id)
        {
            await AuthHelper.CheckUserById(User, id);
            
            await _mediator.Send(new DeleteUserCommand
            {
                UserID = id
            });
        }
        
        [HttpGet]
        [Route("GetUsersByFiltration")]
        public async Task<ActionResult<PaginatedUserResponseDto>> GetUsersByFiltration([FromQuery] FilterUserRequestDto query)
        {
            return await _mediator.Send(new GetUsersByFiltrationQuery{Filter = query});
        }


    }
}
