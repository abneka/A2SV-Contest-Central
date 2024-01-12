using Application.Contracts.Persistence;
using Application.DTOs.User;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Commands.AddProfilePicture;

public class AddProfilePictureCommandHandler : IRequestHandler<AddProfilePictureCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddProfilePictureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(AddProfilePictureCommand request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(user), request.UserId);
        }
        
        var url = await _unitOfWork.FileUpload.UploadImage(request.ImageFile, "UserProfilePictures");

        user.ProfilePicture = url;
        await _unitOfWork.UserRepository.UpdateAsync(user.Id, user);

        return _mapper.Map<UserResponseDto>(user);
    }
}